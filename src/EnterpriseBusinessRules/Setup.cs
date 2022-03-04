using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace EnterpriseBusinessRules
{
    [ExcludeFromCodeCoverage]
    public static class Setup
    {
        public static IServiceCollection AddEnterpriseBusinessRules(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services;  
        }

        public static IApplicationBuilder UseEnterpriseBusinessRules(this IApplicationBuilder app, 
            IWebHostEnvironment env, IConfiguration configuration)
        {
            return app;
        }
    }
}