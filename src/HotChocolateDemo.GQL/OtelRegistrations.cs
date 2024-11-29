using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace HotChocolateDemo.GQL;

public static class OtelRegistrations
{
  public static IHostApplicationBuilder AddOtelServices(this IHostApplicationBuilder builder)
  {
    var otel = builder.Services.AddOpenTelemetry();

    otel.ConfigureResource(r => r.AddService(builder.Environment.ApplicationName));

    // Add Metrics for ASP.NET Core and our custom metrics and export to Prometheus
    otel.WithMetrics(
      m => m
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()

        // Metrics provides by ASP.NET Core in .NET 8
        .AddMeter("Microsoft.AspNetCore.Hosting")
        .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
        .AddPrometheusExporter()
        .AddOtlpExporter()
    );

    otel.WithTracing(
      tr =>
      {
        tr.AddAspNetCoreInstrumentation();
        tr.AddHttpClientInstrumentation();
        tr.AddHotChocolateInstrumentation();
        tr.AddSqlClientInstrumentation();
      }
    );

    builder.Logging.AddOpenTelemetry(
      b =>
      {
        b.IncludeFormattedMessage = true;
        b.IncludeScopes = true;
        b.ParseStateValues = true;
      }
    );

    return builder;
  }
}
