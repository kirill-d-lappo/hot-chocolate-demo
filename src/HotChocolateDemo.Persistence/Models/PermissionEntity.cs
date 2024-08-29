namespace HotChocolateDemo.Persistence.Models;

[AGQLKey("key")]
public class PermissionEntity
{
  [GraphQLKey]
  public long Id { get; set; }

  public string Key { get; set; }

  public List<RoleEntity> Roles { get; set; }

  public List<RolePermissionEntity> RolePermissions { get; set; }
}