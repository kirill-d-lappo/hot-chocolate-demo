using GreenDonut.Projections;
using HotChocolate.Execution.Processing;
using HotChocolate.Pagination;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using HotChocolateDemo.Services.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.GQL.Api.Users;

[QueryType]
public static class UserQuery
{
  [UsePaging]
  [UseProjection]
  [UseFiltering]
  [UseSorting]
  public static IQueryable<UserEntity> AllUsers(HCDemoDbContext dbContext)
  {
    return dbContext.Users
      .AsSplitQuery()
      .AsNoTracking()
      .OrderBy(u => u.Id);
  }

  [Error<UserNotFoundException>]
  public static async Task<UserEntity> UserByUserName(
    string userName,
    IUserByUserNameDataLoader userByUserName,
    ISelection selection,
    PagingArguments pagingArguments,
    CancellationToken ct)
  {
    var user = await userByUserName

      // .WithPagingArguments(pagingArguments)    // waiting for rc1
      .Select(selection)
      .LoadAsync(userName, ct);

    if (user == default)
    {
      throw new UserNotFoundException("No user was found by provided user name");
    }

    return user;
  }
}
