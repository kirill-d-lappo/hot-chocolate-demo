namespace HotChocolateDemo.Services.Users;

public class User
{
  public long Id { get; set; }

  public string UserName { get; set; }

  public DateTimeOffset BirthDateTime { get; set; }
}