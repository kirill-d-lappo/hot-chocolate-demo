using GreenDonut.Projections;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.GQL.Api.Roles;

internal static class RoleDataLoader
{
  [DataLoader(Lookups = [nameof(CreateRoleByIdLookup),])]
  public static async Task<Dictionary<long, RoleEntity>> GetRoleByIdAsync(
    IReadOnlyList<long> ids,
    ISelectorBuilder selectorBuilder,
    IDbContextFactory<HCDemoDbContext> contextFactory,
    CancellationToken ct
  )
  {
    await using var context = await contextFactory.CreateDbContextAsync(ct);

    return await context.Roles
      .AsNoTracking()
      .Select(selectorBuilder)
      .SelectKey(r => r.Id)
      .Where(r => ids.Contains(r.Id))
      .ToDictionaryAsync(b => b.Id, ct);
  }

  public static long CreateRoleByIdLookup(RoleEntity role)
  {
    return role.Id;
  }
}