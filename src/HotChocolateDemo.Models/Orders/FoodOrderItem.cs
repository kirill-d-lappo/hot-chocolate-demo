namespace HotChocolateDemo.Models.Orders;

public class FoodOrderItem
{
  public long Id { get; set; }

  public long? FoodId { get; set; }

  public Food Food { get; set; }

  public long OrderId { get; set; }

  public Order Order { get; set; }

  public int Count { get; set; }
}
