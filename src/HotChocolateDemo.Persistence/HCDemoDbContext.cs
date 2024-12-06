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

    base.OnModelCreating(modelBuilder);
  }
}
