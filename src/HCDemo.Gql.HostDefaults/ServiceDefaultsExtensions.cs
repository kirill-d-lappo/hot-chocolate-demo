using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;

// Adds common .NET Aspire services: service discovery, resilience, health checks, and OpenTelemetry.
// This project should be referenced by each service project in your solution.
// To learn more about using this project, see https://aka.ms/dotnet/aspire/service-defaults
public static class ServiceDefaultsExtensions
{
  public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder)
    where TBuilder : IHostApplicationBuilder
  {
    builder.AddDefaultTelemetry();

    builder.AddDefaultHealthChecks();

    builder.Services.AddServiceDiscovery();

    builder.Services.ConfigureHttpClientDefaults(
      http =>
      {
        // Turn on resilience by default
        http.AddStandardResilienceHandler();

        // Turn on service discovery by default
        http.AddServiceDiscovery();
      }
    );

    return builder;
  }

  public static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder)
    where TBuilder : IHostApplicationBuilder
  {
    var hcBuilder = builder
      .Services
      .AddHealthChecks()

      // Add a default liveness check to ensure app is responsive
      .AddCheck("self", () => HealthCheckResult.Healthy(), ["live",]);

    var hcDemoCs = builder.Configuration.GetConnectionString("HCDemo");
    if (!string.IsNullOrWhiteSpace(hcDemoCs))
    {
      hcBuilder.AddSqlServer(hcDemoCs);
    }

    return builder;
  }

  public static WebApplication MapDefaultEndpoints(this WebApplication app)
  {
    // Adding health checks endpoints to applications in non-development environments has security implications.
    // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
    // All health checks must pass for app to be considered ready to accept traffic after starting

    app.MapHealthChecks("/health");

    // Only health checks tagged with the "live" tag must pass for app to be considered alive
    app.MapHealthChecks(
      "/alive",
      new HealthCheckOptions
      {
        Predicate = r => r.Tags.Contains("live"),
      }
    );

    app.MapPrometheusScrapingEndpoint();

    return app;
  }
}
