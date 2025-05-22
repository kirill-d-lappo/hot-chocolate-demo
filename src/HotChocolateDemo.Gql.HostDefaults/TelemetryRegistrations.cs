using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;

public static class TelemetryRegistrations
{
  public static TBuilder AddDefaultTelemetry<TBuilder>(this TBuilder builder)
    where TBuilder : IHostApplicationBuilder
  {
    var appName = builder.Environment.ApplicationName;
    var envName = builder.Environment.EnvironmentName;
    var isDevelopment = !builder.Environment.IsProduction();

    var otel = builder.Services.AddOpenTelemetry();

    otel = otel.ConfigureResource(
      r => r
        .AddService(appName)
        .AddVersion()
        .AddEnvironment(envName)
        .AddEnvironmentVariableDetector()
    );

    otel = otel.WithMetrics(
      m => m
        .AddRuntimeInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddSqlClientInstrumentation()
        .AddPrometheusExporter()
    );

    otel = otel.WithTracing(
      tr =>
      {
        tr.AddSource(appName);
        tr.AddAspNetCoreInstrumentation();
        tr.AddHttpClientInstrumentation();
        tr.AddHotChocolateInstrumentation();
        tr.AddSqlClientInstrumentation(
          o =>
          {
            // o.EnableConnectionLevelAttributes = isDevelopment;
            o.RecordException = true;
            o.SetDbStatementForText = isDevelopment;
          }
        );
      }
    );

    builder.Logging.AddOpenTelemetry(
      b =>
      {
        b.IncludeFormattedMessage = true;
        b.IncludeScopes = true;
      }
    );

    var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);
    if (useOtlpExporter)
    {
      otel.UseOtlpExporter();
    }

    return builder;
  }
}
