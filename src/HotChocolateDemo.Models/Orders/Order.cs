using System.ComponentModel.DataAnnotations;
using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.Models.Orders;

public class Order
{
  [GraphQLId]
  public long Id { get; set; }

  [MaxLength(64)]
  public string OrderNumber { get; set; }

  public long? UserId { get; set; }

  public User User { get; set; }

  public ICollection<FoodOrderItem> FoodOrderItems { get; set; }

  public ICollection<Food> Foods { get; set; }
}
