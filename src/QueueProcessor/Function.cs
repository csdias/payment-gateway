using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Context;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using QueueProcessor.MessageTracker;
using QueueProcessor.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace QueueProcessor
{
    public class Function
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        static Function()
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            Logging.ConfigureSerilog();
            Log.Information("Lambda starting on {FrameworkDescription}", System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription);
        }

        /// <summary>
        /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
        /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
        /// region the Lambda function is executed in.
        /// </summary>
        public Function()
        {
            var host = Host
                .CreateDefaultBuilder()
                //.ConfigureAppConfiguration((hostingContext, config) =>
                //{
                //    if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
                //    {
                //        config
                //            .AddSystemsManager(
                //                configureSource =>
                //                {
                //                    configureSource.Path =
                //                        $"/aws/reference/secretsmanager/{Environment.GetEnvironmentVariable("SecretsManagerApiUserCredentials")}";
                //                    // Periodically reload in case a rotation has occurred. Old credentials will still work until the following rotation
                //                    // cycle, then they will be removed.
                //                    // https://docs.aws.amazon.com/secretsmanager/latest/userguide/rotating-secrets-two-users.html
                //                    configureSource.ReloadAfter = TimeSpan.FromHours(24);
                //                });
                //    }
                //})
                .ConfigureServices(ConfigureServices)
                .UseSerilog()
                .Build();

            host.Start();
            _serviceProvider = host.Services;
            _logger = Log.ForContext<Function>();

        }

        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used
        /// to respond to SQS messages.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
        {
            using (LogContext.PushProperty("LambdaRequestId", context.AwsRequestId))
            {
                _logger.Information(
                    $"Message received: there are {evnt?.Records?.Count ?? 0} messages in the records collection");

                if (Activity.Current != null)
                    _logger.Warning("An activity already exists with id {ActivityId}", Activity.Current.Id);

                foreach (var message in evnt?.Records ?? Enumerable.Empty<SQSEvent.SQSMessage>())
                {
                    await ProcessMessageAsync(message);
                }
            }
        }

        private async Task ProcessMessageAsync(SQSEvent.SQSMessage message)
        {
            using (LogContext.PushProperty("MessageId", message.MessageId))
            {
                Activity activity = null;

                try
                {
                    activity = new Activity("Receive and Process Outbox messages");

                    if (message.TryGetAttribute("traceparent", out var traceParent))
                        activity.SetParentId(traceParent);

                    activity.Start();

                    _logger.Verbose("Started activity {ActivityId}", activity.Id);
                    var messageConsumer = _serviceProvider.GetService<MessageConsumer>();

                    await messageConsumer.ProcessMessage(message);
                }
                catch (Exception e)
                {
                    _logger.Error(e.Message, e);
                    throw;
                }
                finally
                {
                    if (activity != null)
                    {
                        activity.Stop();
                        _logger.Verbose("Stopped activity {ActivityId}", activity.Id);
                    }
                }
            }
        }

        public virtual void ConfigureServices(HostBuilderContext hostContext, IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings:db");

            services
                .AddScoped<MessageConsumer>()
                .AddScoped<ITrackerRepository, TrackerRepository>()
                .AddScoped<PaymentOrderHandler>()
                .AddDbContext<DbContext>(options =>
                    options.UseNpgsql(connectionString, options =>
                    {
                        options.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
                    }
                    ));
                    //.UseSnakeCaseNamingConvention()); // todo: verify assembly or tool

            services.AddScoped<Func<string, IEventHandler>>(serviceProvider => key =>
             {
                return key switch
                {
                    EventNames.PaymentOrderEvent => serviceProvider.GetService<PaymentOrderHandler>(),
                    _ => throw new ArgumentException($"Unable to resolve Event Handler with key {key}.")
                };
            });
        }
    }
}
