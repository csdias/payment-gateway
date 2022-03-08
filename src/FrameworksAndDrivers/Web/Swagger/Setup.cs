using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System;

namespace FrameworksAndDrivers.Web.Swagger
{
    public static class Setup
    {
        public static IServiceCollection AddFrameworksAndDriversWebSwagger(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment Gateway Api", Version = "v1" });

                // Tell swagger that users need to authenticate via a jwt in a Bearer scheme Authorization header
                // (unless endpoint explicitly marked as allowing anonymous access)
                // Bearer token authentication
                //opts.AddSecurityDefinition("AcmeIdentity", new OpenApiSecurityScheme
                //{
                //    Name = "AcmeIdentity",
                //    BearerFormat = "JWT",
                //    Scheme = "bearer",
                //    Description = "Jwt Authorization token.",
                //    In = ParameterLocation.Cookie,
                //    Type = SecuritySchemeType.Http,
                //});

                // Make sure swagger UI requires a Bearer token specified
                //opts.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    [
                //        new OpenApiSecurityScheme
                //        {
                //            Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme }
                //        }
                //    ] = new List<string>()
                //});
            });

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
