using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.Gql.Handlers.Users.Mutations.UpdateUsers;

public class UpdateUserImageInput
{
  [GraphQLNonNullType]
  [ID<User>]
  public long UserId { get; set; }

  [GraphQLType(typeof(NonNullType<UploadType>))]
  public IFile File { get; set; }
}
