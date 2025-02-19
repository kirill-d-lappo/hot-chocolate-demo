using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.Gql.Handlers.Users.Mutations.CreateUsers;

public class CreateUserInput
{
  [GraphQLNonNullType]
  public string UserName { get; set; }

  public DateTimeOffset? BirthDateTime { get; set; }

  public UserActivityLevel ActivityLevel { get; set; }
}
