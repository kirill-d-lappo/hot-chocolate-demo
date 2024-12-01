namespace HotChocolateDemo.Models.UserManagement;

public class User
{
  [GraphQLId]
  public long Id { get; set; }

  public string UserName { get; set; }

  public DateTimeOffset? BirthDateTime { get; set; }

  public UserActivityLevel ActivityLevel { get; set; }

  public ICollection<Role> Roles { get; set; }
}
