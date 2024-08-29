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

  public DbSet<UserRoleEntity> UserRoles { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema(Schema);

    InitData(modelBuilder);

    base.OnModelCreating(modelBuilder);
  }

  private static void InitData(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<UserEntity>()
      .HasData(
        new UserEntity
        {
          Id = 1,
          UserName = "klappo",
        },
        new UserEntity
        {
          Id = 2,
          UserName = "dmutrov",
        }
      );

    modelBuilder.Entity<RoleEntity>()
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

    modelBuilder.Entity<UserRoleEntity>()
      .HasData(
        new UserRoleEntity()
        {
          UserId = 1,
          RoleId = 1,
        },
        new UserRoleEntity()
        {
          UserId = 1,
          RoleId = 2,
        },
        new UserRoleEntity()
        {
          UserId = 2,
          RoleId = 2,
        }
      );
  }
}
