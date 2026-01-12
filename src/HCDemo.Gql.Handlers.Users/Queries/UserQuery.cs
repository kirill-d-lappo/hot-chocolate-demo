using GreenDonut.Data;
using HotChocolate.Execution.Processing;
using HotChocolate.Types.Pagination;
using HCDemo.Models.UserManagement;
using HCDemo.Persistence.Models.UserManagement;
using HCDemo.Services.UserManagement.Users;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;

namespace HCDemo.Gql.Handlers.Users.Queries;

[QueryType]
public static class UserQuery
{
  // FixMe [2026-01-12 klappo] fix type convertion from UserEntity to User in UserProviderService
  /// <summary>
  /// Searches for all users in the system.
  /// </summary>
  [UsePaging]
  [UseFiltering]
  [UseSorting]
  public static Task<Connection<UserEntity>> AllUsers(
    PagingArguments pagingArgs,
    ISelection selection,
    IFilterContext filterContext,
    ISortingContext sortingContext,
    IUserProviderService userService,
    CancellationToken ct
  )
  {
    return userService
      .FindAllUsersAsync(pagingArgs, filterContext, sortingContext, selection, ct)
      .ToConnectionAsync();
  }

  /// <summary>
  /// Searches for user by its id.
  /// </summary>
  [UseProjection]
  public static Task<User> UserById(
    UserByIdInput input,
    ISelection selection,
    IFindUserByIdDataLoader userService,
    CancellationToken ct
  )
  {
    return userService
      .Select(selection)
      .LoadAsync(input.Id, ct);
  }

  [UseProjection]
  public static async Task<IEnumerable<User>> AllUsersByActivityLevel(
    UserActivityLevel level,
    ISelection selection,
    IUserByActivityLevelDataLoader userService,
    CancellationToken ct
  )
  {
    return await userService
      .Select(selection)
      .LoadAsync(level, ct);
  }
}
