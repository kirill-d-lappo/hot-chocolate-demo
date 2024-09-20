namespace HotChocolateDemo.GQL.Handlers.Users;

public class CreateUserInput
{
  public string UserName { get; set; }

  public DateTimeOffset BirthDateTime { get; set; }
}
