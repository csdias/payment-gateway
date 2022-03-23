using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using EnterpriseBusinessRules;
using ApplicationBusinessRules.UseCases;
using ApplicationBusinessRules.Services;
using ApplicationBusinessRules.Interfaces;
using Amazon.SimpleNotificationService;
using Microsoft.Extensions.DependencyInjection;

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
                .AddScoped<IPaymentService, PaymentService>()
                .AddTransient<IPaymentPublisherService, PaymentPublisherService>()
                .AddAWSService<IAmazonSimpleNotificationService>();
        }

        public static IApplicationBuilder UseApplicationBusinessRules(this IApplicationBuilder app, 
            IWebHostEnvironment env, IConfiguration configuration)
        {
            return app
                .UseEnterpriseBusinessRules(env, configuration);
        }

    }
}
