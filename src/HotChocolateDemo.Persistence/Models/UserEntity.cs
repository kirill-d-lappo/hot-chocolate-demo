using System.ComponentModel.DataAnnotations;

namespace HotChocolateDemo.Persistence.Models;

[AGQLKey("id")]
public class UserEntity
{
  [GraphQLKey]
  public long Id { get; set; }

  [MaxLength(64)]
  public string UserName { get; set; }

  public DateTimeOffset? BirthDate { get; set; }

  public List<RoleEntity> Roles { get; set; }

  public List<UserRoleEntity> UserRoles { get; set; }
}
