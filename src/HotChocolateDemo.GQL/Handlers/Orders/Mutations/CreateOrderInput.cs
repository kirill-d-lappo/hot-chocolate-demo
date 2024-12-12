using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.GQL.Handlers.Orders.Mutations;

public class CreateOrderInput
{
  [ID<User>]
  public long UserId { get; set; }
}
