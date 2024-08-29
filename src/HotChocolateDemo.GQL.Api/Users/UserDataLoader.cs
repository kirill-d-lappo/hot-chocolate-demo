using GreenDonut.Projections;
using HotChocolate.Pagination;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.GQL.Api.Users;

internal static class UserDataLoader
{
  [DataLoader(Lookups = [nameof(CreateUserByIdLookup),])]
  public static async Task<Dictionary<long, UserEntity>> GetUserByIdAsync(
    IReadOnlyList<long> ids,
    ISelectorBuilder selectorBuilder,
    PagingArguments pagingArguments,
    IDbContextFactory<HCDemoDbContext> contextFactory,
    CancellationToken ct)
  {
    await using var context = await contextFactory.CreateDbContextAsync(ct);

    return await context.Users
      .AsNoTracking()
      .Select(selectorBuilder)
      .SelectKey(b => b.Id)
      .Where(r => ids.Contains(r.Id))
      .OrderBy(r => r.Id)
      .ToDictionaryAsync(r => r.Id, ct);

    // .ToBatchPageAsync(t => t.Id, pagingArguments, ct); // waiting for rc1
  }

  public static long CreateUserByIdLookup(UserEntity user)
  {
    return user.Id;
  }

  [DataLoader(Lookups = [nameof(CreateUserByUserNameLookup),])]
  public static async Task<Dictionary<string, UserEntity>> GetUserByUserNameAsync(
    IReadOnlyList<string> usernames,
    ISelectorBuilder selectorBuilder,
    PagingArguments pagingArguments,
    IDbContextFactory<HCDemoDbContext> contextFactory,
    CancellationToken ct)
  {
    await using var context = await contextFactory.CreateDbContextAsync(ct);

    return await context.Users
      .AsNoTracking()
      .Select(selectorBuilder)
      .SelectKey(b => b.Id)
      .Where(r => usernames.Contains(r.UserName))
      .OrderBy(r => r.Id)
      .ToDictionaryAsync(r => r.UserName, ct);

    // .ToBatchPageAsync(t => t.Id, pagingArguments, ct); // waiting for rc1
  }

  public static string CreateUserByUserNameLookup(UserEntity user)
  {
    return user.UserName;
  }
}
