namespace HCDemo.Models.UserManagement;

public class Permission
{
  [GraphQLId]
  public long Id { get; set; }

  public string Key { get; set; }

  public ICollection<Role> Roles { get; set; }
}
