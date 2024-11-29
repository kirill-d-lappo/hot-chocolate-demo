using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;
using HotChocolateDemo.Services.Roles;
using HotChocolateDemo.Services.Users;

namespace HotChocolateDemo.GQL.Handlers.Users;

[ObjectType<User>]
public static partial class UserType
{
  /// <summary>
  /// Roles that were assigned to a user.
  /// </summary>
  [UsePaging]
  public static async Task<Connection<Role>> Roles(
    [Parent] User user,
    PagingArguments pagingArguments,
    IRoleProviderService roleService,
    CancellationToken ct
  )
  {
    return await roleService
      .FindAllByUserIdAsync(user.Id, pagingArguments, ct)
      .ToConnectionAsync();
  }
}
