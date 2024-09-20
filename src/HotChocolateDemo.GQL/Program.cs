using FluentValidation;
using HotChocolateDemo.GQL;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
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
    config.WriteTo.Console(theme: AnsiConsoleTheme.Sixteen);
  }
);

var app = builder.Build();

return await app.RunWithCliAsync(
  args,
  async (app, _, ct) =>
  {
    await MigrateDatabase<HCDemoDbContext>(app, ct);

    app.MapGraphQL();

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
