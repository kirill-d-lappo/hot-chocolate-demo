using HotChocolateDemo.Models.Orders;
using HotChocolateDemo.Models.UserManagement;
using HotChocolateDemo.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Persistence;

public class HCDemoDbContext : DbContext
{
  public const string Schema = "dbo";

  public HCDemoDbContext(DbContextOptions<HCDemoDbContext> options)
    : base(options)
  {
  }

  public DbSet<User> Users { get; set; }

  public DbSet<Role> Roles { get; set; }

  public DbSet<Permission> Permissions { get; set; }

  public DbSet<UserRoleEntity> UserRoles { get; set; }

  public DbSet<RolePermissionEntity> RolePermissions { get; set; }

  public DbSet<Food> Foods { get; set; }

  public DbSet<Order> Orders { get; set; }

  public DbSet<FoodOrderItem> FoodOrderItems { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema(Schema);

    modelBuilder
      .Entity<User>()
      .HasIndex(u => u.UserName)
      .IsUnique();

    modelBuilder
      .Entity<Permission>()
      .HasIndex(u => u.Key)
      .IsUnique();

    modelBuilder
      .Entity<User>()
      .HasMany(e => e.Roles)
      .WithMany(e => e.Users)
      .UsingEntity<UserRoleEntity>();

    modelBuilder
      .Entity<Role>()
      .HasMany(e => e.Permissions)
      .WithMany(e => e.Roles)
      .UsingEntity<RolePermissionEntity>();

    modelBuilder
      .Entity<Order>()
      .HasMany(e => e.Foods)
      .WithMany(e => e.Orders)
      .UsingEntity<FoodOrderItem>(
        l => l
          .HasOne(vav => vav.Food)
          .WithMany(n => n.FoodOrderItems)
          .OnDelete(DeleteBehavior.ClientSetNull),
        r => r
          .HasOne(vav => vav.Order)
          .WithMany(n => n.FoodOrderItems)
          .OnDelete(DeleteBehavior.Cascade)
      );

    InitData(modelBuilder);

    base.OnModelCreating(modelBuilder);
  }

  private static void InitData(ModelBuilder modelBuilder)
  {
    modelBuilder
      .Entity<User>()
      .HasData(
        new User
        {
          Id = 1,
          UserName = "klappo",
          BirthDateTime = DateTimeOffset.UtcNow.AddYears(-20),
          ActivityLevel = UserActivityLevel.Advanced,
        },
        new User
        {
          Id = 2,
          UserName = "dmutrov",
          BirthDateTime = DateTimeOffset.UtcNow.AddYears(-25),
          ActivityLevel = UserActivityLevel.Pro,
        }
      );

    modelBuilder
      .Entity<Role>()
      .HasData(
        new Role
        {
          Id = 1,
          Name = "admin",
        },
        new Role
        {
          Id = 2,
          Name = "driver",
        }
      );

    modelBuilder
      .Entity<UserRoleEntity>()
      .HasData(
        new UserRoleEntity
        {
          UserId = 1,
          RoleId = 1,
        },
        new UserRoleEntity
        {
          UserId = 1,
          RoleId = 2,
        },
        new UserRoleEntity
        {
          UserId = 2,
          RoleId = 2,
        }
      );

    modelBuilder
      .Entity<Permission>()
      .HasData(
        new Permission
        {
          Id = 1,
          Key = "create:user",
        },
        new Permission
        {
          Id = 2,
          Key = "update:user",
        },
        new Permission
        {
          Id = 3,
          Key = "delete:user",
        },
        new Permission
        {
          Id = 4,
          Key = "delete:user",
        }
      );

    modelBuilder
      .Entity<RolePermissionEntity>()
      .HasData(
        new RolePermissionEntity
        {
          PermissionId = 1,
          RoleId = 1,
        },
        new RolePermissionEntity
        {
          PermissionId = 2,
          RoleId = 1,
        },
        new RolePermissionEntity
        {
          PermissionId = 3,
          RoleId = 1,
        },
        new RolePermissionEntity
        {
          PermissionId = 3,
          RoleId = 2,
        }
      );
  }
}
