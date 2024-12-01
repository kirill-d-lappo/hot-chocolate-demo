using System.ComponentModel.DataAnnotations;

namespace HotChocolateDemo.Models.Orders;

public class Order
{
  [GraphQLId]
  public long Id { get; set; }

  [MaxLength(64)]
  public string OrderNumber { get; set; }

  public ICollection<FoodOrderItem> FoodOrderItems { get; set; }

  public ICollection<Food> Foods { get; set; }
}
