using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Persistence.Models;

[PrimaryKey(nameof(UserId), nameof(RoleId))]
public class UserRoleEntity
{
  [GraphQLKey]
  [ForeignKey(nameof(User))]
  public long UserId { get; set; }

  [GraphQLKey]
  [ForeignKey(nameof(Role))]
  public long RoleId { get; set; }

  public UserEntity User { get; set; }

  public RoleEntity Role { get; set; }
}