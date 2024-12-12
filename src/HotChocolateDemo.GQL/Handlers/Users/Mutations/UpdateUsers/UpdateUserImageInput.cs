using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.GQL.Handlers.Users.Mutations.UpdateUsers;

public class UpdateUserImageInput
{
  [GraphQLNonNullType]
  [ID<User>]
  public long UserId { get; set; }

  [GraphQLType(typeof(NonNullType<UploadType>))]
  public IFile File { get; set; }
}
