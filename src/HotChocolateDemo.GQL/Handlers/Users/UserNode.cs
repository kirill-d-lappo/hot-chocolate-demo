using HotChocolateDemo.Models.UserManagement;
using HotChocolateDemo.Services.UserManagement.Users;

namespace HotChocolateDemo.GQL.Handlers.Users;

[ObjectType<User>]
public static partial class UserNode
{
  [NodeResolver]
  public static async Task<User> FindUserById(long id, IFindUserByIdDataLoader userProvider, CancellationToken ct)
  {
    return await userProvider.LoadAsync(id, ct);
  }
}
