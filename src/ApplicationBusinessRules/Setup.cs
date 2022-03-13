using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EnterpriseBusinessRules;
using ApplicationBusinessRules.UseCases;
using ApplicationBusinessRules.Services;
using ApplicationBusinessRules.Interfaces;

namespace ApplicationBusinessRules
{
    public static class Setup
    {
        public static IServiceCollection AddApplicationBusinessRules(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddEnterpriseBusinessRules(configuration)
                .AddScoped<IGetPaymentUseCase, GetPaymentUseCase>()
                .AddScoped<ICreatePaymentUseCase, CreatePaymentUseCase>()
                .AddScoped<IUpdatePaymentStatusUseCase, UpdatePaymentStatusUseCase>()
                .AddScoped<IValidateCreditCardUseCase, ValidateCreditCardUseCase>()
                .AddScoped<IPaymentQueueProcessorService, PaymentQueueProcessorService>()
                .AddScoped<IPaymentService, PaymentService>();
        }

        public static IApplicationBuilder UseApplicationBusinessRules(this IApplicationBuilder app, 
            IWebHostEnvironment env, IConfiguration configuration)
        {
            return app
                .UseEnterpriseBusinessRules(env, configuration);
        }

    }
}
