namespace HotChocolateDemo.GQL.Handlers.Users.Queries;

public class UserByUserNameInput
{
  [GraphQLNonNullType]
  public string UserName { get; set; }
}
