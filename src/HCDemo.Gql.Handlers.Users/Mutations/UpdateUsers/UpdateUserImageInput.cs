using HCDemo.Models.UserManagement;

namespace HCDemo.Gql.Handlers.Users.Mutations.UpdateUsers;

public class UpdateUserImageInput
{
  [GraphQLNonNullType]
  public long UserId { get; set; }

  [GraphQLType(typeof(NonNullType<UploadType>))]
  public IFile File { get; set; }
}
