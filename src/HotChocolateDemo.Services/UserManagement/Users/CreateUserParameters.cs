using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.Services.UserManagement.Users;

public class CreateUserParameters
{
  public string UserName { get; set; }

  public DateTimeOffset? BirthDateTime { get; set; }

  public UserActivityLevel ActivityLevel { get; set; }
}
