using HotChocolateDemo.Models.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Persistence;

public static class HCDemoDbContextDataSeed
{
  public static DbContextOptionsBuilder UseInitData(this DbContextOptionsBuilder optionsBuilder)
  {
    return optionsBuilder
      .UseSeeding(InitData)
      .UseAsyncSeeding(
        (context, b, _) =>
        {
          InitData(context, b);

          return Task.CompletedTask;
        }
      );
  }

  private static void InitData(DbContext context, bool hadStoreManagementOperation)
  {
    if (context is not HCDemoDbContext dbContext)
    {
      return;
    }

    var hasData = dbContext.Permissions.Any(b => b.Key == "create:user");
    if (hasData)
    {
      return;
    }

    var permissions = new Permission[]
    {
      new()
      {
        Key = "create:user",
      },
      new()
      {
        Key = "update:user",
      },
      new()
      {
        Key = "delete:user",
      },
      new()
      {
        Key = "read:user",
      },
    };

    dbContext.Permissions.AddRange(permissions);

    var roles = new Role[]
    {
      new()
      {
        Name = "admin",
        Permissions = [permissions[0], permissions[1], permissions[2],],
      },
      new()
      {
        Name = "driver",
        Permissions = [permissions[2],],
      },
    };

    dbContext.Roles.AddRange(roles);

    var users = new User[]
    {
      new()
      {
        UserName = "klappo",
        BirthDateTime = DateTimeOffset.UtcNow.AddYears(-20),
        ActivityLevel = UserActivityLevel.Advanced,
        Roles = [roles[0], roles[1],],
      },
      new()
      {
        UserName = "dmutrov",
        BirthDateTime = DateTimeOffset.UtcNow.AddYears(-25),
        ActivityLevel = UserActivityLevel.Pro,
        Roles = [roles[1],],
      },
    };

    dbContext.Users.AddRange(users);

    dbContext.SaveChanges();
  }
}
