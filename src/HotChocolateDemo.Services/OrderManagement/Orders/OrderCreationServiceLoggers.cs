using HotChocolateDemo.Persistence.Models.Orders;
using Microsoft.Extensions.Logging;

namespace HotChocolateDemo.Services.OrderManagement.Orders;

internal static partial class OrderCreationServiceLoggers
{
  [LoggerMessage(Level = LogLevel.Information, Message = "Order was created: {@Order}")]
  public static partial void OrderWasCreated(this ILogger logger, OrderEntity order);
}
