using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Persistence.Models;

[PrimaryKey(nameof(UserId), nameof(RoleId))]
public class UserRoleEntity
{
  [GraphQLKeyAttribute()]
  public long UserId { get; set; }

  [GraphQLKeyAttribute()]
  public long RoleId { get; set; }

  public UserEntity User { get; set; }

  public RoleEntity Role { get; set; }
}
