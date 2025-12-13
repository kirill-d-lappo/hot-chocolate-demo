using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HCDemo.Persistence;

/// <summary>
/// For <c>dotnet ef </c> commands.
/// </summary>
public class HCDemoDbContextFactory : IDesignTimeDbContextFactory<HCDemoDbContext>
{
  public HCDemoDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<HCDemoDbContext>();
    var config = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json", true)
      .AddEnvironmentVariables()
      .Build();

    var connectionString = config.GetConnectionString(SqlServerConfigurations.ConnectionName);
    if (string.IsNullOrWhiteSpace(connectionString))
    {
      connectionString = "DataSource=dummy";
    }

    optionsBuilder.UseHCDemoDb(connectionString);

    return new HCDemoDbContext(optionsBuilder.Options);
  }
}
