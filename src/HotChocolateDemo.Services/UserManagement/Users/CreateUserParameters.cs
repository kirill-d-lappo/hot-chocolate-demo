namespace HotChocolateDemo.Services.UserManagement.Users;

public class CreateUserParameters
{
  public string UserName { get; set; }

  public DateTimeOffset BirthDateTime { get; set; }
}
