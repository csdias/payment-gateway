using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace FrameworksAndDrivers.Web.Swagger
{
    public static class Setup
    {
        public static IServiceCollection AddFrameworksAndDriversWebSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment Gateway Api", Version = "v1" });
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
