using GreenDonut.Selectors;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Services.Roles;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.GQL.Handlers.Roles;

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
      .Select(
        r => new Role
        {
          Id = r.Id,
          Name = r.Name,
        }
      )
      .Select(r => r.Id, selectorBuilder)
      .ToDictionaryAsync(b => b.Id, ct);
  }
}
