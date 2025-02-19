using System.ComponentModel.DataAnnotations;
using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.Persistence.Models.UserManagement;

public class UserEntity
{
  [Key]
  public long Id { get; set; }

  [MaxLength(64)]
  public string UserName { get; set; }

  public DateTimeOffset? BirthDateTime { get; set; }

  public UserActivityLevel ActivityLevel { get; set; }

  public ICollection<RoleEntity> Roles { get; set; }

  [MaxLength(64)]
  public string ImageFileName { get; set; }
}
