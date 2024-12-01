using System.ComponentModel.DataAnnotations.Schema;
using HotChocolateDemo.Models.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Persistence.Models;

[PrimaryKey(nameof(UserId), nameof(RoleId))]
public class UserRoleEntity
{
  [ForeignKey(nameof(User))]
  public long UserId { get; set; }

  [ForeignKey(nameof(Role))]
  public long RoleId { get; set; }

  public User User { get; set; }

  public Role Role { get; set; }
}
