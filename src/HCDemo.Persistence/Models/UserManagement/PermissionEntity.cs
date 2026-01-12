namespace HCDemo.Persistence.Models.UserManagement;

public class PermissionEntity
{
  [Key]
  public long Id { get; set; }

  [MaxLength(64)]
  public string Key { get; set; }

  public ICollection<RoleEntity> Roles { get; set; }
}
