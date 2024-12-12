namespace HotChocolateDemo.Services.OrderManagement.Orders.Events;

public class NewOrderWasCreatedMessage
{
  public long OrderId { get; set; }

  public string OrderNumber { get; set; }
}
