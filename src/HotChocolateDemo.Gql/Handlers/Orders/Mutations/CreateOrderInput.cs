using HotChocolateDemo.Models.Orders;
using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.Gql.Handlers.Orders.Mutations;

public class CreateOrderInput
{
  [ID<User>]
  public long UserId { get; set; }

  public List<long> FoodIds { get; set; }
}
