using Agilosoft.AgileTimeKeeper.Api.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agilosoft.AgileTimeKeeper.Api.Model;
using log4net;
using log4net.Config;
using System.IO;
using log4net.Repository;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Agilosoft.AgileTimeKeeper.Api.Middlewares;
using Agilosoft.AgileTimeKeeper.Api.HubConfig;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Agilosoft.AgileTimeKeeper.Api.Services;

namespace Agilosoft.AgileTimeKeeper.Api
{
    public class Startup
    {
        public static ILoggerRepository repository;
        public static string applicationURL;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            repository = LogManager.CreateRepository("NETCORERepository");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            applicationURL = Configuration.GetConnectionString("ApplicationURL");
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.WithOrigins(applicationURL)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
            services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
            services.AddScoped<IRandomPasswordGenerator, RandomPasswordGenerator>();
            services.AddScoped<ExecutorController>(provider => new ExecutorController(Configuration));
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            
            //JWT Authentication
            var appSettings = appSettingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.key);
            services.AddAuthentication(au =>
            {
                au.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                au.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer("Yahoo",options =>
            //    {
            //        options.Audience = Configuration.GetValue<string>("JWT:ClientId");
            //        options.Authority = Configuration.GetValue<string>("JWT:Issuer");
            //        options.Events = new JwtBearerEvents
            //        {
            //            OnTokenValidated = (ctx) =>
            //            {
            //                var email = ctx.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value.ToString();
            //                // using email check the user is Admin from Employee table
            //                var role = "";
            //                role = new ExecutorController(Configuration).GetRole(email).ToString();
            //                if (role != "")
            //                {
            //                    var identity = (ctx.Principal.Identity as ClaimsIdentity);
            //                    (ctx.Principal.Identity as ClaimsIdentity).AddClaim(new Claim(identity.RoleClaimType, "User"));

            //                    if (role == "Admin")
            //                    {

            //                        (ctx.Principal.Identity as ClaimsIdentity).AddClaim(new Claim(identity.RoleClaimType, "Admin"));
            //                    }
            //                }
            //                return System.Threading.Tasks.Task.CompletedTask;
            //            },

            //        };
            //    });

            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<Email>();
            services.AddSingleton(emailConfig);
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).
                AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
            services.AddSignalR();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseMiddleware<ReverseProxyMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notification");
            });
        }
    }
}
