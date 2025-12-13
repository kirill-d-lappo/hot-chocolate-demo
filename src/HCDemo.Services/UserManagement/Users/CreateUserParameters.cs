using HCDemo.Models.UserManagement;

namespace HCDemo.Services.UserManagement.Users;

public class CreateUserParameters
{
  public string UserName { get; set; }

  public DateTimeOffset? BirthDateTime { get; set; }

  public UserActivityLevel ActivityLevel { get; set; }
}
