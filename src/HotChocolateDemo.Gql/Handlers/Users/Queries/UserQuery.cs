using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;
using HotChocolateDemo.Models.UserManagement;
using HotChocolateDemo.Services.UserManagement.Users;

namespace HotChocolateDemo.Gql.Handlers.Users.Queries;

[QueryType]
public static class UserQuery
{
  /// <summary>
  /// Searches for all users in the system.
  /// </summary>
  [UsePaging]
  public static Task<Connection<User>> AllUsers(
    PagingArguments pagingArgs,
    ISelection selection,
    IUserProviderService userService,
    CancellationToken ct
  )
  {
    return userService
      .FindAllUsersAsync(pagingArgs, selection.AsSelector<User>(), ct)
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
