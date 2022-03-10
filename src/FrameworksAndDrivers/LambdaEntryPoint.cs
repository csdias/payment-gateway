using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Context;
using Serilog.Core.Enrichers;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;

namespace FrameworksAndDrivers
{
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        static LambdaEntryPoint()
        {
            // Use W3C Trace Context format for correlation ids in HTTP headers and logs
            // https://devblogs.microsoft.com/aspnet/improvements-in-net-core-3-0-for-troubleshooting-and-monitoring-distributed-apps/
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
            Logging.ConfigureSerilog();
            Log.Information("Lambda starting on {FrameworkDescription}", System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription);
        }

        public LambdaEntryPoint()
        {
            // Binary data needs to be returned from Lambda as base64 encoded string.
            // Base class ensures this happens for most content types, but favicon needs manually adding.
            RegisterResponseContentEncodingForContentType("image/x-icon", ResponseContentEncoding.Base64);
        }

        protected override void Init(IHostBuilder builder) =>
            builder.UseSerilog();

        protected override void Init(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(appConfigBuilder =>
                {
                    appConfigBuilder
                        .AddSystemsManager(configureSource =>
                        {
                            configureSource.Path =
                                $"/aws/reference/secretsmanager/{Environment.GetEnvironmentVariable("SecretsManagerApiUserCredentials")}";
                            // Periodically reload in case a rotation has occurred. Old credentials will still work until the following rotation
                            // cycle, then they will be removed.
                            // https://docs.aws.amazon.com/secretsmanager/latest/userguide/rotating-secrets-two-users.html
                            configureSource.ReloadAfter = TimeSpan.FromHours(24);
                        });
                })
                .UseStartup<Startup>();
        }

        public override Task<APIGatewayProxyResponse> FunctionHandlerAsync(APIGatewayProxyRequest request, ILambdaContext lambdaContext)
        {
            // Include Lambda & API Gateway request ids on all log entries written during the request
            using (LogContext.Push(
                new PropertyEnricher("LambdaRequestId", lambdaContext.AwsRequestId),
                new PropertyEnricher("ApiGatewayRequestId", request.RequestContext.RequestId)))
            {
                return base.FunctionHandlerAsync(request, lambdaContext);
            }
        }

        protected override void PostMarshallRequestFeature(IHttpRequestFeature aspNetCoreRequestFeature, APIGatewayProxyRequest apiGatewayProxyRequest, ILambdaContext lambdaContext)
        {
            aspNetCoreRequestFeature.PathBase = Environment.GetEnvironmentVariable("GatewayPrefix") ?? "";

            // The minus one is ensure path is always at least set to `/`
            aspNetCoreRequestFeature.Path =
                aspNetCoreRequestFeature.Path.Substring(aspNetCoreRequestFeature.PathBase.Length - 1);
            lambdaContext.Logger.LogLine($"Path: {aspNetCoreRequestFeature.Path}, PathBase: {aspNetCoreRequestFeature.PathBase}");
        }
    }
}
