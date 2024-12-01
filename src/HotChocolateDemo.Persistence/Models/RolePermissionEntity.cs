using System.ComponentModel.DataAnnotations.Schema;
using HotChocolateDemo.Models.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Persistence.Models;

[PrimaryKey(nameof(RoleId), nameof(PermissionId))]
public class RolePermissionEntity
{
  [ForeignKey(nameof(Role))]
  public long RoleId { get; set; }

  [ForeignKey(nameof(Permission))]
  public long PermissionId { get; set; }

  public Role Role { get; set; }

  public Permission Permission { get; set; }
}
