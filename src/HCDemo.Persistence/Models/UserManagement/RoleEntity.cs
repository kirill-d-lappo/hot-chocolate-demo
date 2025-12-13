namespace HCDemo.Persistence.Models.UserManagement;

public class RoleEntity
{
  [Key]
  public long Id { get; set; }

  public string Name { get; set; }

  public ICollection<PermissionEntity> Permissions { get; set; }

  public ICollection<UserEntity> Users { get; set; }
}
