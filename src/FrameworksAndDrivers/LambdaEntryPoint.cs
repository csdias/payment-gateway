using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.AspNetCoreServer;
using Amazon.Lambda.Core;
using Microsoft.AspNetCore.Hosting;
using Serilog.Context;
using Serilog.Core.Enrichers;

namespace FrameworksAndDrivers
{
    public class LambdaEntryPoint : APIGatewayHttpApiV2ProxyFunction
    {
        protected override void Init(IWebHostBuilder builder)
        {
            builder.UseStartup<Startup>();
        }

        public override Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandlerAsync(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext lambdaContext)
        {
            // Include Lambda & API Gateway request ids on all log entries written during the request
            using (LogContext.Push(
                new PropertyEnricher("LambdaRequestId", lambdaContext.AwsRequestId),
                new PropertyEnricher("ApiGatewayRequestId", request.RequestContext.RequestId)))
            {
                return base.FunctionHandlerAsync(request, lambdaContext);
            }
        }

    }
}
