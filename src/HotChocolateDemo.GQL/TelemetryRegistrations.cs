using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace HotChocolateDemo.GQL;

public static class TelemetryRegistrations
{
  public static IHostApplicationBuilder AddTelemetry(this IHostApplicationBuilder builder)
  {
    var otel = builder.Services.AddOpenTelemetry();

    otel = otel.ConfigureResource(
      r => r
        .AddService(builder.Environment.ApplicationName)
        .AddVersion()
        .AddEnvironment(builder.Environment.EnvironmentName)
        .AddEnvironmentVariableDetector()
    );

    // Add Metrics for ASP.NET Core and our custom metrics and export to Prometheus
    otel = otel.WithMetrics(
      m => m
        .AddRuntimeInstrumentation()
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()

        // Metrics provides by ASP.NET Core in .NET 8
        .AddMeter("Microsoft.AspNetCore.Hosting")
        .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
        .AddPrometheusExporter()
        .AddOtlpExporter()
    );

    otel = otel.WithTracing(
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

public static class OtelBuilderExtensionsRegistrations
{
  public static ResourceBuilder AddVersion(this ResourceBuilder builder, string version = null)
  {
    version ??= Assembly
      .GetExecutingAssembly()
      .GetName()
      .Version
      ?.ToString();

    if (string.IsNullOrWhiteSpace(version))
    {
      return builder;
    }

    return builder.AddAttribute("service.version", version);
  }

  public static ResourceBuilder AddEnvironment(this ResourceBuilder builder, string environmentName)
  {
    return builder.AddAttribute("service.environment", environmentName);
  }

  public static ResourceBuilder AddAttribute(this ResourceBuilder builder, string key, string value)

  {
    return builder.AddAttributes([new KeyValuePair<string, object>(key, value),]);
  }
}
