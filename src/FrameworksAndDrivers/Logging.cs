using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace FrameworksAndDrivers
{
    public static class Logging
    {
        /// <summary>
        /// Remove SQL command text in order to keep down log file size
        /// </summary>
        private class RemoveSqlEnricher : ILogEventEnricher
        {
            public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
            {
                if (logEvent.Properties.TryGetValue("SourceContext", out var sourceContext))
                {
                    if (sourceContext.ToString() == "\"Microsoft.EntityFrameworkCore.Database.Command\"")
                    {
                        logEvent.RemovePropertyIfPresent("commandText");
                    }
                }
            }
        }

        public static void ConfigureSerilog()
        {
            var devEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            var logConfig = new LoggerConfiguration();

            // 1. Log-levels
            //    Allow a global override via environment variable
            if (Environment.GetEnvironmentVariable("LOG_LEVEL_OVERRIDE") is { } logLevel &&
                Enum.TryParse(logLevel, true, out LogEventLevel level))
                logConfig = logConfig.MinimumLevel.Is(level);
            else
                //    Otherwise, set things up such that we get:
                logConfig = logConfig
                    .MinimumLevel.Information() // 1 log entry per incoming Request (via Serilog's RequestLogging middleware)
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // Framework or system-level errors
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information) // 1 log entry per EF db query
                    .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Information) // 1 log entry per outgoing HttpClient Request
                    .Filter.ByExcluding(
                        "StartsWith(SourceContext, 'System.Net.Http.HttpClient') and not (EventId.Id = 101 and EndsWith(SourceContext, 'ClientHandler'))")
                    .Enrich.With<RemoveSqlEnricher>();

            // 2. Outputs
            logConfig = logConfig
            // Summaries to Console
                .WriteTo.Console(new RenderedCompactJsonFormatter());
            // Summaries to Debug when in dev environment
            if (devEnvironment)  
            {
                logConfig = logConfig
                    .WriteTo.Debug()
                    .WriteTo.File(
                        new RenderedCompactJsonFormatter(),
                        "C:\\logs\\PaymentGateway.log",
                        fileSizeLimitBytes: 10 * 1024 * 1024, // 10MB in dev
                        shared: true,
                        flushToDiskInterval: TimeSpan.FromSeconds(2),
                        rollOnFileSizeLimit: true,
                        retainedFileCountLimit: 15);
            }

            Log.Logger = logConfig
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        public static void ConfigureRequestLogging(RequestLoggingOptions opts)
        {
            opts.EnrichDiagnosticContext = (logContext, httpContext) =>
            {
                // Serilog will log RequestMethod, RequestPath, StatusCode and Elapsed automatically

                logContext.Set("RequestHost", httpContext.Request.Host);
                logContext.Set("RequestProtocol", httpContext.Request.Protocol);
                logContext.Set("RequestScheme", httpContext.Request.Scheme);
                logContext.Set("RequestSourceIp", httpContext.Connection.RemoteIpAddress);
                logContext.Set("RequestForwardedFor", httpContext.Request.Headers["X-Forwarded-For"]);
                logContext.Set("RequestForwardedHost", httpContext.Request.Headers["X-Forwarded-Host"]);
                logContext.Set("RequestForwardedProtocol", httpContext.Request.Headers["X-Forwarded-Proto"]);

                if (httpContext.GetEndpoint() is { } endpoint)
                    logContext.Set("EndpointName", endpoint.DisplayName);

                if (httpContext.User != null)
                {
                    void SetIfClaimExists(string claimType, string propertyName)
                    {
                        var claim = httpContext.User.FindFirst(claimType);
                        if (claim != null)
                            logContext.Set(propertyName, claim.Value);
                    }
                    SetIfClaimExists("Acme_AccountId", "UserAcmeAccountId");
                    SetIfClaimExists("Acme_SystemId", "UserSystemId");
                    SetIfClaimExists("Acme_SubSystemId", "UserSubSystemId");
                    SetIfClaimExists("Acme_SystemName", "UserSystemName");
                    // NameIdentifier will either be:
                    // a GigyaId for individual users,
                    // an empty guid for Systems not proxying a user request,
                    // or {SystemId}--{UserId} for Systems proxying a user request
                    var nameIdClaimValue = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                    if (nameIdClaimValue != null)
                    {
                        if (httpContext.User.HasClaim("Acme_JwtType", "SystemIdentity")) // System
                        {
                            var match = Regex.Match(nameIdClaimValue, "^.*--(?<id>.*)$");
                            if (match.Success)
                            {
                                logContext.Set("UserId", match.Groups["id"].Value);
                            }
                        }
                        else // Individual user
                        {
                            logContext.Set("UserId", nameIdClaimValue);
                        }
                    }
                }
            };

            // Default behaviour is to log every request,
            // but we don't really need that for monitoring endpoints, like the health check.
            // So - provide an override mechanism via RequestLogLevelAttribute.
            opts.GetLevel = (context, _, exception) =>
            {
                if (exception != null || context.Response.StatusCode > 499)
                    return LogEventLevel.Error;

                if (context.GetEndpoint()?.Metadata.GetMetadata<RequestLogLevelAttribute>() is {} overrideAttribute)
                    return overrideAttribute.Level;

                return LogEventLevel.Information;
            };
        }

        /// <summary>
        /// Add MVC-specific data to Serilog's per-request log entry
        /// </summary>
        public class SerilogMvcLoggingFilter : IActionFilter
        {
            private readonly IDiagnosticContext _diagnosticContext;
            public SerilogMvcLoggingFilter(IDiagnosticContext diagnosticContext)
            {
                _diagnosticContext = diagnosticContext;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                _diagnosticContext.Set("MvcRouteData", context.ActionDescriptor.RouteValues);
                _diagnosticContext.Set("MvcActionName", context.ActionDescriptor.DisplayName);
                _diagnosticContext.Set("MvcActionId", context.ActionDescriptor.Id);
                _diagnosticContext.Set("MvcModelStateIsValid", context.ModelState.IsValid);
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
            }
        }

        /// <summary>
        /// Specifies the level at which to log summary information about successful requests.
        /// Only needed if a level other than the default - <see cref="LogEventLevel.Information"/> - is required.
        /// Failed requests (those that throw an exception or result in a 5xx status code) are always logged at <see cref="LogEventLevel.Error"/> level.
        /// </summary>
        [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
        public class RequestLogLevelAttribute : Attribute
        {
            public LogEventLevel Level { get; set; } = LogEventLevel.Information;
        }
    }
}
