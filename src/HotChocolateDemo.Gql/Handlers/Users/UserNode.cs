using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.Gql.Handlers.Users;

[ObjectType<User>]
public static partial class UserNode
{
  // Note [2025-02-19 klappo] uncomment to add user to Node type set
  // [NodeResolver]
  // public static async Task<User> FindUserById(long id, IFindUserByIdDataLoader userProvider, CancellationToken ct)
  // {
  //   return await userProvider.LoadAsync(id, ct);
  // }
}
