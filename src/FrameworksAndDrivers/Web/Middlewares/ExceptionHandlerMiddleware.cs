using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FrameworksAndDrivers.Web.Middlewares.Entities;

namespace FrameworksAndDrivers.Web.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ErrorHandlingMiddleware(
            RequestDelegate next, 
            IWebHostEnvironment env,
            ILoggerFactory logFactory          
        )
        {
            this._env = env;
            this._next = next;
            this._logger = logFactory.CreateLogger<ErrorHandlingMiddleware>();
        }           

        public async Task InvokeAsync(HttpContext context)
        {
            try 
            {
                await this._next(context);
            }
            catch (Exception exception) 
            {               
                await this.HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var responseError = new ReponseErrorEntity()
            {
                Path = context.Request.Path.ToString(),
                Message = exception.Message,
                Method = context.Request.Method,
                Status = (int)HttpStatusCode.InternalServerError,
                RequestId = context.TraceIdentifier,
                Stack = exception.StackTrace
            };

            this._logger.LogError(responseError.ToString());

            if (!this._env.IsDevelopment())
            {
                responseError.Message = "Internal Server Error";
                responseError.Stack = null;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = responseError.Status;
            
            return context.Response.WriteAsync(responseError.ToJson());
        }
    }
}