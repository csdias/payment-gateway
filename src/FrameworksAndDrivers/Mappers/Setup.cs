using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FrameworksAndDrivers.Mappers
{
    public static class Setup
    {
        public static IServiceCollection AddFrameworksAndDriversMappers(this IServiceCollection services, IConfiguration configuration)
        {                   
            services
              .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;

        }

        public static IApplicationBuilder UseFrameworksAndDriversMappers(this IApplicationBuilder app, 
            IWebHostEnvironment env, IConfiguration configuration)
        {
            return app;
        }
    }
}
