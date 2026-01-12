using HCDemo.Models.UserManagement;

namespace HCDemo.Gql.Handlers.Orders.Mutations;

public class CreateOrderInput
{
  [ID<User>]
  public long UserId { get; set; }

  public List<long> FoodIds { get; set; }
}
