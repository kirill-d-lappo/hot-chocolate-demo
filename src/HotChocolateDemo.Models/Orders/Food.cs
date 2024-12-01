using System.ComponentModel.DataAnnotations;

namespace HotChocolateDemo.Models.Orders;

public class Food
{
  [GraphQLId]
  public long Id { get; set; }

  [MaxLength(64)]
  public string Name { get; set; }

  public ICollection<FoodOrderItem> FoodOrderItems { get; set; }

  public ICollection<Order> Orders { get; set; }
}
