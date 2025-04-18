using Microsoft.Extensions.Logging;

namespace HotChocolateDemo.Services.OrderManagement.Orders;

internal static partial class OrderCreationServiceLoggers
{
  [LoggerMessage(Level = LogLevel.Information, Message = "Order was created: {OrderId}")]
  public static partial void OrderWasCreated(this ILogger logger, long orderId);
}
