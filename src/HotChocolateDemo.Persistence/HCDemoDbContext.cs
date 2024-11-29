using HotChocolateDemo.Models;
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

  public DbSet<UserEntity> Users { get; set; }

  public DbSet<RoleEntity> Roles { get; set; }

  public DbSet<PermissionEntity> Permissions { get; set; }

  public DbSet<UserRoleEntity> UserRoles { get; set; }

  public DbSet<RolePermissionEntity> RolePermissions { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema(Schema);

    modelBuilder
      .Entity<UserEntity>()
      .HasIndex(u => u.UserName)
      .IsUnique();

    modelBuilder
      .Entity<UserEntity>()
      .HasMany(e => e.Roles)
      .WithMany(e => e.Users)
      .UsingEntity<UserRoleEntity>();

    modelBuilder
      .Entity<RoleEntity>()
      .HasMany(e => e.Permissions)
      .WithMany(e => e.Roles)
      .UsingEntity<RolePermissionEntity>();

    InitData(modelBuilder);

    base.OnModelCreating(modelBuilder);
  }

  private static void InitData(ModelBuilder modelBuilder)
  {
    modelBuilder
      .Entity<UserEntity>()
      .HasData(
        new UserEntity
        {
          Id = 1,
          UserName = "klappo",
          BirthDate = DateTimeOffset.UtcNow.AddYears(-20),
          ActivityLevel = UserActivityLevel.Advanced,
        },
        new UserEntity
        {
          Id = 2,
          UserName = "dmutrov",
          BirthDate = DateTimeOffset.UtcNow.AddYears(-25),
          ActivityLevel = UserActivityLevel.Pro,
        }
      );

    modelBuilder
      .Entity<RoleEntity>()
      .HasData(
        new RoleEntity
        {
          Id = 1,
          Name = "admin",
        },
        new RoleEntity
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
      .Entity<PermissionEntity>()
      .HasData(
        new PermissionEntity
        {
          Id = 1,
          Key = "create:user",
        },
        new PermissionEntity
        {
          Id = 2,
          Key = "update:user",
        },
        new PermissionEntity
        {
          Id = 3,
          Key = "delete:user",
        },
        new PermissionEntity
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
