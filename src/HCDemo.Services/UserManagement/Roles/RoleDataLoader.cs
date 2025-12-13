using System.Linq.Expressions;
using GreenDonut.Data;
using HCDemo.Models.UserManagement;
using HCDemo.Persistence;
using HCDemo.Persistence.Models.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace HCDemo.Services.UserManagement.Roles;

internal static class RoleDataLoader
{
  [DataLoader]
  public static async Task<Dictionary<long, Role>> GetRoleByIdAsync(
    IReadOnlyList<long> ids,
    ISelectorBuilder selectorBuilder,
    IDbContextFactory<HCDemoDbContext> contextFactory,
    CancellationToken ct
  )
  {
    await using var context = await contextFactory.CreateDbContextAsync(ct);

    return await context
      .Roles
      .AsNoTracking()
      .Where(r => ids.Contains(r.Id))
      .Select(RoleSelector)
      .Select(r => r.Id, selectorBuilder)
      .ToDictionaryAsync(b => b.Id, ct);
  }

  [DataLoader]
  public static async Task<ILookup<long, Role>> FindAllRolesByUserIdAsync(
    IReadOnlyList<long> userIds,
    ISelectorBuilder selectorBuilder,
    IDbContextFactory<HCDemoDbContext> contextFactory,
    CancellationToken ct
  )
  {
    await using var context = await contextFactory.CreateDbContextAsync(ct);

    return context
      .UserRoles
      .AsNoTracking()
      .Where(ur => userIds.Contains(ur.UserId))
      .Select(ur => ur.Role)
      .Select(RoleSelector)
      .Select(r => r.Id, selectorBuilder)
      .ToLookup(b => b.Id);
  }

  private static Expression<Func<RoleEntity, Role>> RoleSelector { get; } = r => new Role
  {
    Id = r.Id,
    Name = r.Name,
  };
}
