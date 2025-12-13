using System.ComponentModel.DataAnnotations;
using HCDemo.Models.UserManagement;

namespace HCDemo.Models.Orders;

public class Order
{
  public long Id { get; set; }

  [MaxLength(64)]
  public string OrderNumber { get; set; }

  public OrderCreationSource CreationSource { get; set; }

  public long? UserId { get; set; }

  public User User { get; set; }

  public ICollection<FoodOrderItem> FoodOrderItems { get; set; }

  public ICollection<Food> Foods { get; set; }
}
