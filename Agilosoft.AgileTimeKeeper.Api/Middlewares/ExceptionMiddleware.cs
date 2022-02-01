using Agilosoft.AgileTimeKeeper.Api.Business_Exceptions;
using Agilosoft.AgileTimeKeeper.Api.Errors;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Agilosoft.AgileTimeKeeper.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        private readonly ILog _log = LogManager.GetLogger(Startup.repository.Name, typeof(ExceptionMiddleware));
        private static JObject errorResponseList;

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            this._next = next;
            this._env = env;
            errorResponseList = JObject.Parse(File.ReadAllText(@"Errors\ErrorResponse.json"));
        }

        private Error GetErrorResponse(Exception ex)
        {

            HttpStatusCode statusCode = 0;
            string message = "";
            Error response;
            JObject errorResponse;
            Type errorType = ex.GetType();
            errorResponse = (JObject)errorResponseList.GetValue(errorType.Name.ToString());
            statusCode = (HttpStatusCode)((int)errorResponse.GetValue("ErrorCode"));
            if ((int)statusCode > 500050)
            {
                message = ex.ToString();
            }
            else
            {
                message = errorResponse.GetValue("ErrorMessage").ToString();
            }
            if (_env.IsDevelopment())
            {
                response = new Error((int)statusCode, ex.Message, "Error", ex.StackTrace?.ToString());
            }
            else
            {
                response = new Error((int)statusCode, message, "Error");
            }
            return response;
        }

        public async Task Invoke(HttpContext context)
        {
            var User = context.User;
            
            try
            {
                await _next(context);
            }
            catch (SqlException ex)
            {
                Error errorResponse;
                if ((int)ex.Number == 500052)
                {

                    errorResponse = GetErrorResponse(new RecordNotFoundException(ex.Message));
                }
                else if ((int)ex.Number > 500050)
                {
                    errorResponse = GetErrorResponse(new DuplicationException(ex.Message));
                }
                else
                {
                    errorResponse = GetErrorResponse(ex);
                }
                _log.Error(ex.Message);
                await context.Response.WriteAsync(errorResponse.ToString());
            }
            catch (Exception ex)
            {
                Error errorResponse = GetErrorResponse(ex);
                _log.Error(ex.Message);
                await context.Response.WriteAsync(errorResponse.ToString());
            }
        }
    }
}
