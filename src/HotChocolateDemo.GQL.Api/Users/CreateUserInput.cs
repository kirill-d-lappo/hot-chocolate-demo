namespace HotChocolateDemo.GQL.Api.Users.Creations;

public class CreateUserInput
{
  public string UserName { get; set; }

  public DateTimeOffset BirthDateTime { get; set; }
}
