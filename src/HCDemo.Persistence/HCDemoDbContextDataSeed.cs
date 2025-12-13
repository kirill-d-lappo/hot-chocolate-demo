using HCDemo.Models.Orders;
using HCDemo.Models.UserManagement;
using HCDemo.Persistence.Models.Orders;
using HCDemo.Persistence.Models.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace HCDemo.Persistence;

public static class HCDemoDbContextDataSeed
{
  public static DbContextOptionsBuilder UseInitData(this DbContextOptionsBuilder optionsBuilder)
  {
    return optionsBuilder
      .UseSeeding(InitData)
      .UseAsyncSeeding((context, b, _) =>
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

    var permissions = new PermissionEntity[]
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

    var roles = new RoleEntity[]
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

    var users = new UserEntity[]
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

    var foods = new FoodEntity[]
    {
      new()
      {
        Name = "Big Shnitzel",
      },
      new()
      {
        Name = "Chicken Salad",
      },
      new()
      {
        Name = "Chili",
      },
      new()
      {
        Name = "Chili",
      },
    };

    dbContext.Foods.AddRange(foods);

    var orders = new OrderEntity[]
    {
      new()
      {
        OrderNumber = Guid
          .NewGuid()
          .ToString("N"),
        FoodOrderItems = foods
          .Select(f => new FoodOrderItemEntity
            {
              Food = f,
            }
          )
          .ToList(),
        CreationSource = OrderCreationSource.Unknown,
      },
    };

    dbContext.Orders.AddRange(orders);

    dbContext.SaveChanges();
  }
}
