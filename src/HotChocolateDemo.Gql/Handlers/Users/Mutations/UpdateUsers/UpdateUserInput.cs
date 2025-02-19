using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.Gql.Handlers.Users.Mutations.UpdateUsers;

public class UpdateUserInput
{
  [ID<User>]
  [GraphQLNonNullType]
  public long Id { get; set; }

  public Optional<DateTimeOffset?> BirthDateTime { get; set; }

  [DefaultValue(UserActivityLevel.Basic)]
  public Optional<UserActivityLevel> ActivityLevel { get; set; }
}
