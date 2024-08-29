using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Persistence.Models;

[PrimaryKey(nameof(RoleId), nameof(PermissionId))]
public class RolePermissionEntity
{
  [GraphQLKey]
  [ForeignKey(nameof(Role))]
  public long RoleId { get; set; }

  [GraphQLKey]
  [ForeignKey(nameof(Permission))]
  public long PermissionId { get; set; }

  public RoleEntity Role { get; set; }

  public PermissionEntity Permission { get; set; }
}