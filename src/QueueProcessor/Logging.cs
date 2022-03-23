using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System;
using System.Diagnostics;

namespace QueueProcessor
{
    public static class Logging
    {
        public static void ConfigureSerilog()
        {
            var logConfig = new LoggerConfiguration();

            // Log levels:
            // By default write Information level logs, but suppress those from the framework except for a single one to log outbound HTTP calls
            // Allow global overridding via an environment variable
            if (Environment.GetEnvironmentVariable("LOG_LEVEL_OVERRIDE") is { } logLevel &&
                Enum.TryParse(logLevel, true, out LogEventLevel level))
                logConfig = logConfig.MinimumLevel.Is(level);
            else
                logConfig = logConfig
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .MinimumLevel.Override("System.Net.Http.HttpClient", LogEventLevel.Information)
                    .Filter.ByExcluding("StartsWith(SourceContext, 'System.Net.Http.HttpClient') and not (EventId.Id = 101 and EndsWith(SourceContext, 'ClientHandler'))");

            Log.Logger = logConfig
                // Log output:
                // Since we're running in Lambda, we just log to stdout / the console, using our standard json format
                .WriteTo.Console(new RenderedCompactJsonFormatter())
                // Enrichers:
                // Include any properties we've added via LogContext.Push
                .Enrich.FromLogContext()
                // Include tracing ids
                .Enrich.With<CurrentActivityEnricher>()
                .CreateLogger();
        }
    }

    // Add tracing ids to all Serilog log events,
    // replicating the out-of-the-box functionality in a web application.
    // This means we don't need to remember to manually create new LogContexts
    // (there's also a nuget package for this, but it's so trivial it seems like overkill: https://github.com/RehanSaeed/Serilog.Enrichers.Span)
    public class CurrentActivityEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var activity = Activity.Current;
            if (activity == null) return;

            logEvent.AddPropertyIfAbsent(new LogEventProperty("TraceId",
                new ScalarValue(activity.TraceId.ToHexString())));
            logEvent.AddPropertyIfAbsent(new LogEventProperty("SpanId",
                new ScalarValue(activity.SpanId.ToHexString())));
            logEvent.AddPropertyIfAbsent(new LogEventProperty("ParentId",
                new ScalarValue(activity.ParentSpanId.ToHexString())));
        }
    }
}
