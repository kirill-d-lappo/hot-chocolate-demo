using Microsoft.EntityFrameworkCore;

namespace HCDemo.Persistence.Models.UserManagement;

[PrimaryKey(nameof(UserId), nameof(RoleId))]
public class UserRoleEntity
{
  [ForeignKey(nameof(User))]
  public long UserId { get; set; }

  [ForeignKey(nameof(Role))]
  public long RoleId { get; set; }

  public UserEntity User { get; set; }

  public RoleEntity Role { get; set; }
}
