using System.ComponentModel.DataAnnotations;

namespace HCDemo.Models.Orders;

public class Food
{
  public long Id { get; set; }

  [MaxLength(64)]
  public string Name { get; set; }

  public ICollection<FoodOrderItem> FoodOrderItems { get; set; }

  public ICollection<Order> Orders { get; set; }
}
