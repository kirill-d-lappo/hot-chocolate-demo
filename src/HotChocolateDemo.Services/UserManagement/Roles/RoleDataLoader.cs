using GreenDonut.Selectors;
using HotChocolateDemo.Models.UserManagement;
using HotChocolateDemo.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Services.UserManagement.Roles;

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
      .Select(r => r.Id, selectorBuilder)
      .ToLookup(b => b.Id);
  }
}
