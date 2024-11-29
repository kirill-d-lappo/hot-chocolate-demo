using HotChocolate.Data.Filters;
using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;
using HotChocolateDemo.Services.Users;

namespace HotChocolateDemo.GQL.Handlers.Users.Queries;

[QueryType]
public static class UserQuery
{
  /// <summary>
  /// Searches for all users in the system.
  /// </summary>
  [UsePaging]
  [UseProjection]
  [UseFiltering]
  [UseSorting]
  public static Task<Connection<User>> AllUsers(
    PagingArguments pagingArgs,
    IFilterContext filterContext,
    IUserProviderService userService,
    CancellationToken ct
  )
  {
    return userService
      .FindAllUsersAsync(pagingArgs, null, filterContext, ct)
      .ToConnectionAsync();
  }

  /// <summary>
  /// Searches for user by its id.
  /// </summary>
  [UseProjection]
  public static Task<User> UserById(UserByIdInput input, IUserProviderService userService, CancellationToken ct)
  {
    return userService.FindUserByIdAsync(input.Id, ct);
  }
}
