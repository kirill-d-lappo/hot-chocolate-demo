using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolateDemo.Persistence;

public static class HCDemoDbRegistrationExtensions
{
  public static void AddHCDemoPersistence(this IServiceCollection services)
  {
    services.AddPooledDbContextFactory<HCDemoDbContext>(ConfigureDbContext);
  }

  private static void ConfigureDbContext(IServiceProvider serviceProvider, DbContextOptionsBuilder options)
  {
    options = options.EnableDetailedErrors();
    options = options.EnableSensitiveDataLogging();

    options.UseHCDemoDb();
  }
}
