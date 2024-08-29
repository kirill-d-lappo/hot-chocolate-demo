using FluentValidation;
using HotChocolateDemo.GQL.Api;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Formatting.Display;
using Serilog.Sinks.SystemConsole.Themes;

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
  });

var app = builder.Build();

if (!IsGraphQLCommand(args))
{
  await MigrateDatabase<HCDemoDbContext>(app);
}

app.MapGraphQL();

app.RunWithGraphQLCommands(args);

return;

static async Task MigrateDatabase<TDbContext>(IHost app)
  where TDbContext : DbContext
{
  using var scope = app.Services
    .CreateScope();

  await using var dbContext = await scope
    .ServiceProvider
    .GetRequiredService<IDbContextFactory<TDbContext>>()
    .CreateDbContextAsync();

  await dbContext.Database.MigrateAsync();
}

static bool IsGraphQLCommand(string[] args)
{
  return args is ["schema", ..,];
}
