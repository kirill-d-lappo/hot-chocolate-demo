using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HotChocolateDemo.Persistence;

internal static class SqlServerConfigurations
{
  public const string ConnectionName = "HCDemo";

  public static DbContextOptionsBuilder UseHCDemoDb(
    this DbContextOptionsBuilder optionsBuilder,
    string connectionString = null
  )
  {
    if (string.IsNullOrWhiteSpace(connectionString))
    {
      connectionString = $"name={ConnectionName}";
    }

    return optionsBuilder
      .UseSqlServer(connectionString, ConfigureSqlServer)
      .UseInitData();
  }

  private static void ConfigureSqlServer(SqlServerDbContextOptionsBuilder b)
  {
    b.MigrationsHistoryTable("__EFMigrationsHistory", HCDemoDbContext.Schema);
  }
}
