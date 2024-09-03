using FluentValidation;
using HotChocolateDemo.GQL.Api;
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
    });

var app = builder.Build();

await RunAsync(app, args, async (app, args, ct) =>
{
    await MigrateDatabase<HCDemoDbContext>(app, ct);

    app.MapGraphQL();

    await app.RunAsync(ct);

    return;

    static async Task MigrateDatabase<TDbContext>(IHost app, CancellationToken ct)
        where TDbContext : DbContext
    {
        using var scope = app.Services
            .CreateScope();

        await using var dbContext = await scope
            .ServiceProvider
            .GetRequiredService<IDbContextFactory<TDbContext>>()
            .CreateDbContextAsync(ct);

        await dbContext.Database.MigrateAsync(ct);
    }
}, ct);

return;

static async Task RunAsync(
    WebApplication app,
    string[] args,
    Func<WebApplication, string[], CancellationToken, Task> appAction,
    CancellationToken ct)
{
    if (IsGraphQLCommand(args))
    {
        await app.RunWithGraphQLCommandsAsync(args);

        return;
    }

    await appAction(app, args, ct);

    return;

    static bool IsGraphQLCommand(string[] args)
    {
        return args is ["schema", ..,];
    }
}