using System.ComponentModel.DataAnnotations;

namespace HCDemo.Models.UserManagement;

public class User
{
  // [GraphQLId]
  // [GraphQLKey] // alias to HotChocolate.ApolloFederation.Types.KeyAttribute
  public long Id { get; set; }

  public string UserName { get; set; }

  public DateTimeOffset? BirthDateTime { get; set; }

  public UserActivityLevel ActivityLevel { get; set; }

  public ICollection<Role> Roles { get; set; }

  [MaxLength(64)]
  public string ImageFileName { get; set; }
}
