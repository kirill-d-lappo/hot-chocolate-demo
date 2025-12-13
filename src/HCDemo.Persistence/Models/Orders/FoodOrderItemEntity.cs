namespace HCDemo.Persistence.Models.Orders;

public class FoodOrderItemEntity
{
  [Key]
  public long Id { get; set; }

  public long? FoodId { get; set; }

  public FoodEntity Food { get; set; }

  public long OrderId { get; set; }

  public OrderEntity Order { get; set; }

  public int Count { get; set; }
}
