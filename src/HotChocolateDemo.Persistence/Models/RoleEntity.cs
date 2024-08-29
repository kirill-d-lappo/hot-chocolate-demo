using System.ComponentModel.DataAnnotations;

namespace HotChocolateDemo.Persistence.Models;

[AGQLKey("id")]
public class RoleEntity
{
  [GraphQLKey]
  public long Id { get; set; }

  [MaxLength(64)]
  public string Name { get; set; }

  public List<UserEntity> Users { get; set; }

  public List<UserRoleEntity> UserRoles { get; set; }
}
