using System.ComponentModel.DataAnnotations;
using HotChocolateDemo.Persistence.Models.UserManagement;

namespace HotChocolateDemo.Persistence.Models.Orders;

public class OrderEntity
{
  [Key]
  public long Id { get; set; }

  [MaxLength(64)]
  public string OrderNumber { get; set; }

  public long? UserId { get; set; }

  public UserEntity User { get; set; }

  public ICollection<FoodOrderItemEntity> FoodOrderItems { get; set; }

  public ICollection<FoodEntity> Foods { get; set; }
}
