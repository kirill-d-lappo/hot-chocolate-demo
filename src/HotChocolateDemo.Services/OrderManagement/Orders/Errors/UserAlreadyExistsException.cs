namespace HotChocolateDemo.Services.OrderManagement.Orders.Errors;

public class OrderCreationException : Exception
{
  public OrderCreationException()
    : base(FormatMessage())

  {
  }

  public OrderCreationException(Exception innerException)
    : base(FormatMessage(), innerException)
  {
  }

  private static string FormatMessage()
  {
    return $"Error at creating order";
  }
}
