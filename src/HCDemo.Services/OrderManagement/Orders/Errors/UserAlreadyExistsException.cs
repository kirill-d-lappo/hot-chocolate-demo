namespace HCDemo.Services.OrderManagement.Orders.Errors;

public class OrderCreationException : Exception
{
  public OrderCreationException()
    : base(FormatMessage(string.Empty))

  {
  }

  public OrderCreationException(Exception innerException)
    : base(FormatMessage(innerException.InnerException?.Message ?? innerException.Message), innerException)
  {
  }

  private static string FormatMessage(string message)
  {
    return $"Error at creating order: {message}";
  }
}
