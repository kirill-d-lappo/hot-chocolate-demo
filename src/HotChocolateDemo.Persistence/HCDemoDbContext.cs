using HotChocolateDemo.Persistence.Models.Orders;
using HotChocolateDemo.Persistence.Models.UserManagement;
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

  public DbSet<FoodEntity> Foods { get; set; }

  public DbSet<OrderEntity> Orders { get; set; }

  public DbSet<FoodOrderItemEntity> FoodOrderItems { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema(Schema);

    modelBuilder
      .Entity<UserEntity>()
      .HasIndex(u => u.UserName)
      .IsUnique();

    modelBuilder
      .Entity<PermissionEntity>()
      .HasIndex(u => u.Key)
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

    modelBuilder
      .Entity<OrderEntity>()
      .HasMany(e => e.Foods)
      .WithMany(e => e.Orders)
      .UsingEntity<FoodOrderItemEntity>(
        l => l
          .HasOne(vav => vav.Food)
          .WithMany(n => n.FoodOrderItems)
          .OnDelete(DeleteBehavior.ClientSetNull),
        r => r
          .HasOne(vav => vav.Order)
          .WithMany(n => n.FoodOrderItems)
          .OnDelete(DeleteBehavior.Cascade)
      );

    base.OnModelCreating(modelBuilder);
  }
}
