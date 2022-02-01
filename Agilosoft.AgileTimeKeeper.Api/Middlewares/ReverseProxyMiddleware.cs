using Agilosoft.AgileTimeKeeper.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Agilosoft.AgileTimeKeeper.Api.Middlewares
{
    public class ReverseProxyMiddleware
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly RequestDelegate _nextMiddleware;
        private readonly IConfiguration _configurationSettings;
        private IConfigurationSection _proxySetting;
        
        public ReverseProxyMiddleware(RequestDelegate nextMiddleware,IConfiguration optionsSettings)
        {
            _nextMiddleware = nextMiddleware;
            _configurationSettings = optionsSettings;
        }

        public async Task Invoke(HttpContext context)
        {
            if (_configurationSettings.GetValue<bool>("ReverseProxy:Enabled"))
            {
                var targetUri = BuildTargetUri(context.Request);

                if (targetUri != null && _proxySetting !=null)
                {
                    var targetRequestMessage = CreateTargetMessage(context, targetUri);

                    using (var responseMessage = await _httpClient.SendAsync(targetRequestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
                    {
                        context.Response.StatusCode = (int)responseMessage.StatusCode;
                        CopyFromTargetResponseHeaders(context, responseMessage);
                        await ProcessResponseContent(context, responseMessage);
                    }
                    return;
                }
            }
            await _nextMiddleware(context);
        }

        private async Task ProcessResponseContent(HttpContext context, HttpResponseMessage responseMessage)
        {
            var content = await responseMessage.Content.ReadAsByteArrayAsync();
            if (_proxySetting.GetValue<bool>("URLSpoofSettings:Enabled") && IsContentOfType(responseMessage, _proxySetting.GetValue<string>("URLSpoofSettings:ContentType")))
            {
                var stringContent = Encoding.UTF8.GetString(content);




                stringContent = stringContent.Replace( _proxySetting["To"], context.Request.Scheme+"://"+ context.Request.Host + _proxySetting["From"]);
                JObject obj = JObject.Parse(stringContent);
                foreach (var exceptKey in _proxySetting.GetSection("URLSpoofSettings:Except").Get<string[]>())
                {

                    if (obj.ContainsKey(exceptKey))
                        obj[exceptKey] = obj[exceptKey].ToString().Replace(context.Request.Scheme + "://"+ context.Request.Host + _proxySetting["From"], _proxySetting["To"]);
                }
                foreach (var addKey in _proxySetting.GetSection("URLSpoofSettings:Add").GetChildren().ToList())
                {
                    var key = addKey["key"];
                    var value = addKey["value"];
                    if (!obj.ContainsKey(key))
                        obj[key] = value;
                }

                var newContent = obj.ToString(Formatting.None);

                await context.Response.WriteAsync(newContent, Encoding.UTF8);
                return;
            }
            await context.Response.Body.WriteAsync(content);
            
        }
        private bool IsContentOfType(HttpResponseMessage responseMessage, string type)
        {
            var result = false;

            if (responseMessage.Content?.Headers?.ContentType != null)
            {
                result = responseMessage.Content.Headers.ContentType.MediaType == type;
            }

            return result;
        }

        private HttpRequestMessage CreateTargetMessage(HttpContext context, Uri targetUri)
        {
            var requestMessage = new HttpRequestMessage();
            CopyFromOriginalRequestContentAndHeaders(context, requestMessage);

            requestMessage.RequestUri = targetUri;
            requestMessage.Headers.Host = targetUri.Host;
            requestMessage.Method = GetMethod(context.Request.Method);

            return requestMessage;
        }

        private void CopyFromOriginalRequestContentAndHeaders(HttpContext context, HttpRequestMessage requestMessage)
        {
            var requestMethod = context.Request.Method;

            if (!HttpMethods.IsGet(requestMethod) &&
              !HttpMethods.IsHead(requestMethod) &&
              !HttpMethods.IsDelete(requestMethod) &&
              !HttpMethods.IsTrace(requestMethod))
            {
                var streamContent = new StreamContent(context.Request.Body);
                requestMessage.Content = streamContent;
            }

            foreach (var header in context.Request.Headers)
            {
                requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }
        }

        private void CopyFromTargetResponseHeaders(HttpContext context, HttpResponseMessage responseMessage)
        {
            foreach (var header in responseMessage.Headers)
            {
                context.Response.Headers[header.Key] = header.Value.ToArray();
            }

            foreach (var header in responseMessage.Content.Headers)
            {
                if (!header.Key.Equals("Content-Length"))
                    context.Response.Headers[header.Key] = header.Value.ToArray();
            }
            context.Response.Headers.Remove("transfer-encoding");
        }
        private static HttpMethod GetMethod(string method)
        {
            if (HttpMethods.IsDelete(method)) return HttpMethod.Delete;
            if (HttpMethods.IsGet(method)) return HttpMethod.Get;
            if (HttpMethods.IsHead(method)) return HttpMethod.Head;
            if (HttpMethods.IsOptions(method)) return HttpMethod.Options;
            if (HttpMethods.IsPost(method)) return HttpMethod.Post;
            if (HttpMethods.IsPut(method)) return HttpMethod.Put;
            if (HttpMethods.IsTrace(method)) return HttpMethod.Trace;
            return new HttpMethod(method);
        }

        private Uri BuildTargetUri(HttpRequest request)
        {
            var proxyList = _configurationSettings.GetSection("ReverseProxy:ProxySettings").GetChildren().ToList();
            Uri targetUri = null;
            foreach (var proxySetting in proxyList) 
            {
                if (request.Path.StartsWithSegments(proxySetting.GetValue<string>("From"), out var remainingPath))
                {
                    _proxySetting = proxySetting;
                    targetUri = new Uri(proxySetting.GetValue<string>("To") + remainingPath);
                    break;
                }
            }
            return targetUri;
        }
    }
}


