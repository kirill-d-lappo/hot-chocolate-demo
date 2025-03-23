using System.ComponentModel.DataAnnotations;

namespace HotChocolateDemo.Persistence.Models.Orders;

public class FoodEntity
{
  [Key]
  public long Id { get; set; }

  [MaxLength(64)]
  public string Name { get; set; }

  public ICollection<FoodOrderItemEntity> FoodOrderItems { get; set; }
}
