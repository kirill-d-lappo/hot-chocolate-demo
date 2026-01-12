using FluentValidation;
using HCDemo.Gql.Api;
using HCDemo.Persistence;
using HCDemo.Services;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHCDemoPersistence();
builder.Services.AddHCDemoServices();

builder.Services.AddGqlServices();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Logging.ClearProviders();
builder.Host.UseSerilog(
  (context, config) =>
  {
    config.ReadFrom.Configuration(context.Configuration);
    config.WriteTo.Console(theme: AnsiConsoleTheme.Sixteen);
  },
  writeToProviders: true
);

builder.AddServiceDefaults();

var app = builder.Build();

return await app.RunWithConsoleCancellationAsync(async (app, ct) =>
  {
    if (app.Environment.IsDevelopment() || app.Configuration.GetValue("DOTNET_RUNNING_IN_CONTAINER", false))
    {
      await app.MigrateDatabaseAsync<HCDemoDbContext>(ct);
    }

    return await app.RunWithGqlCliAsync(
      args,
      async (app, args, ct) =>
      {
        app.MapDefaultEndpoints();

        const string graphQlPath = "/graphql";
        app.MapGraphQL(graphQlPath);
        app.MapGetRedirect(graphQlPath);

        await app.RunAsync(ct);

        return 0;
      },
      ct
    );
  }
);
