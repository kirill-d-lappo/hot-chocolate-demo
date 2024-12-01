namespace HotChocolateDemo.Models.UserManagement;

public class Role
{
  [GraphQLId]
  public long Id { get; set; }

  public string Name { get; set; }

  public ICollection<Permission> Permissions { get; set; }

  public ICollection<User> Users { get; set; }
}
