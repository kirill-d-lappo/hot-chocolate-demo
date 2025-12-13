using Microsoft.EntityFrameworkCore;

namespace HCDemo.Gql.Api;

public static class MigrationExtensions
{
  public static async Task MigrateDatabaseAsync<TDbContext>(this IHost app, CancellationToken ct)
    where TDbContext : DbContext
  {
    using var scope = app.Services.CreateScope();

    await using var dbContext = await scope
      .ServiceProvider
      .GetRequiredService<IDbContextFactory<TDbContext>>()
      .CreateDbContextAsync(ct);

    await dbContext.Database.MigrateAsync(ct);
  }
}
