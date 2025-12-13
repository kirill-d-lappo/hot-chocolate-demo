using HCDemo.Models.Orders;

namespace HCDemo.Services.OrderManagement.Orders;

public class CreateOrderParameters
{
  public long UserId { get; set; }

  public OrderCreationSource CreationSource { get; set; }

  public List<long> FoodIds { get; set; }
}
