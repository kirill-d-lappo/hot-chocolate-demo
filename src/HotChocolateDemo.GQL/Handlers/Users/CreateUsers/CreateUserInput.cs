namespace HotChocolateDemo.GQL.Handlers.Users.CreateUsers;

public class CreateUserInput
{
  [GraphQLNonNullType]
  public string UserName { get; set; }

  public DateTimeOffset BirthDateTime { get; set; }
}
