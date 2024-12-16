using FluentValidation;
using HotChocolateDemo.GQL;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Services;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHCDemoPersistence();
builder.Services.AddHCDemoServices();

builder.Services.AddGqlServices();
builder.Services.AddHealthChecks();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Host.UseSerilog(
  (context, config) =>
  {
    config.ReadFrom.Configuration(context.Configuration);
    config.WriteTo.Console(theme: AnsiConsoleTheme.Sixteen);
  }
);

builder.AddTelemetry();

var app = builder.Build();

return await app.RunWithConsoleCancellationAsync(
  async (app, ct) =>
  {
#if DEBUG
    if (app.Environment.IsDevelopment())
    {
      await app.MigrateDatabaseAsync<HCDemoDbContext>(ct);
    }
#endif

    return await app.RunWithGqlCliAsync(
      args,
      async (app, args, ct) =>
      {
        app.MapHealthChecks("/healthz");
        app.MapPrometheusScrapingEndpoint();
        app.MapGraphQL();
        app.MapGet(
          "/",
          context =>
          {
            context.Response.Redirect("./graphql", false);

            return Task.CompletedTask;
          }
        );

        await app.RunAsync(ct);

        return 0;
      },
      ct
    );
  }
);
