namespace HotChocolateDemo.Gql.Handlers.Users.Queries;

public class UserByUserNameInput
{
  [GraphQLNonNullType]
  public string UserName { get; set; }
}
