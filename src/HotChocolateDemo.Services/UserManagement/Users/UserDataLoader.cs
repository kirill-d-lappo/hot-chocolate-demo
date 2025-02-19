using GreenDonut.Data;
using HotChocolateDemo.Models.UserManagement;
using HotChocolateDemo.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Services.UserManagement.Users;

internal static class UserDataLoader
{
  [DataLoader]
  public static async Task<Dictionary<long, User>> FindUserByIdAsync(
    IReadOnlyList<long> ids,
    ISelectorBuilder selectorBuilder,
    IDbContextFactory<HCDemoDbContext> contextFactory,
    CancellationToken ct
  )
  {
    await using var context = await contextFactory.CreateDbContextAsync(ct);

    return await context
      .Users
      .AsNoTracking()
      .OrderBy(r => r.Id)
      .Where(r => ids.Contains(r.Id))
      .Select(b => b.Id, selectorBuilder)
      .ToDictionaryAsync(u => u.Id, ct);
  }

  [DataLoader]
  public static async Task<Dictionary<string, Page<User>>> FindUserByUserNameAsync(
    IReadOnlyList<string> usernames,
    ISelectorBuilder selectorBuilder,
    PagingArguments pagingArguments,
    IDbContextFactory<HCDemoDbContext> contextFactory,
    CancellationToken ct
  )
  {
    await using var context = await contextFactory.CreateDbContextAsync(ct);

    return await context
      .Users
      .AsNoTracking()
      .OrderBy(r => r.Id)
      .Where(r => usernames.Contains(r.UserName))
      .Select(b => b.Id, selectorBuilder)
      .ToBatchPageAsync(t => t.UserName, pagingArguments, ct);
  }

  [DataLoader]
  public static async Task<ILookup<UserActivityLevel, User>> GetUserByActivityLevelAsync(
    IReadOnlyList<UserActivityLevel> levels,
    ISelectorBuilder selectorBuilder,
    IDbContextFactory<HCDemoDbContext> contextFactory,
    CancellationToken ct
  )
  {
    await using var context = await contextFactory.CreateDbContextAsync(ct);

    return context
      .Users
      .AsNoTracking()
      .Where(r => levels.Contains(r.ActivityLevel))
      .Select(b => b.Id, selectorBuilder)
      .ToLookup(t => t.ActivityLevel);
  }
}
