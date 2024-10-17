using FluentValidation;
using HotChocolateDemo.GQL;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;

var cts = new CancellationTokenSource();
var ct = cts.Token;
Console.CancelKeyPress += (s, e) =>
{
  cts.Cancel();
  e.Cancel = true;
};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHCDemoPersistence();
builder.Services.AddHCDemoServices();

builder.Services.AddGqlServices();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Host.UseSerilog(
  (context, config) =>
  {
    config.ReadFrom.Configuration(context.Configuration);
    if (context.HostingEnvironment.IsDevelopment())
    {
      config.WriteTo.Console(theme: AnsiConsoleTheme.Sixteen);
    }
    else
    {
      config.WriteTo.Console(new RenderedCompactJsonFormatter());
    }
  }
);

var app = builder.Build();

return await app.RunWithCliAsync(
  args,
  async (app, _, ct) =>
  {
#if DEBUG
    if (app.Environment.IsDevelopment())
    {
      await MigrateDatabase<HCDemoDbContext>(app, ct);
    }
#endif

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

    return;

    static async Task MigrateDatabase<TDbContext>(IHost app, CancellationToken ct)
      where TDbContext : DbContext
    {
      using var scope = app.Services.CreateScope();

      await using var dbContext = await scope
       .ServiceProvider
       .GetRequiredService<IDbContextFactory<TDbContext>>()
       .CreateDbContextAsync(ct);

      await dbContext.Database.MigrateAsync(ct);
    }
  },
  ct
);
