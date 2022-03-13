using YamlDotNet.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FrameworksAndDrivers.Web.Swagger
{
    public static class Setup
    {
        {
            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment Gateway Api", Version = "v1" });
            });

            return services;
        {     
            return services;              
        }

        public static IApplicationBuilder UseFrameworksAndDriversWebSwagger(this IApplicationBuilder app,
            IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "Payment Gateway Api");
                c.RoutePrefix = string.Empty;
            });

            return app;
        }
    }
}
