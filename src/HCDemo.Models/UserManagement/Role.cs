namespace HCDemo.Models.UserManagement;

public class Role
{
  // [GraphQLId]
  // [GraphQLKey] // alias to HotChocolate.ApolloFederation.Types.KeyAttribute
  public long Id { get; set; }

  public string Name { get; set; }

  public ICollection<Permission> Permissions { get; set; }

  public ICollection<User> Users { get; set; }
}
