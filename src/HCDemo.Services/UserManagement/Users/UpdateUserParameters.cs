using HCDemo.Models.UserManagement;

namespace HCDemo.Services.UserManagement.Users;

public class UpdateUserParameters
{
  public long Id { get; set; }

  public Optionated<DateTimeOffset?> BirthDateTime { get; set; }

  public Optionated<UserActivityLevel> ActivityLevel { get; set; }
}
