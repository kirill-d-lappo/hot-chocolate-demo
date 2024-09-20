namespace HotChocolateDemo.GQL.Handlers.Users;

public class UserByUserNameInput
{
  [GraphQLNonNullType]
  public string UserName { get; set; }
}
