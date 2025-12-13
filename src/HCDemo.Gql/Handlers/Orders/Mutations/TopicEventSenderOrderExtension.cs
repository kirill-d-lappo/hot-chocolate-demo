using HotChocolate.Subscriptions;
using HCDemo.Services.OrderManagement.Orders.Events;

namespace HCDemo.Gql.Handlers.Orders.Mutations;

internal static class TopicEventSenderOrderExtension
{
  public static ValueTask SendOrderCreatedAsync(this ITopicEventSender sender, long orderId, CancellationToken ct)
  {
    return sender.SendAsync(
      TopicNames.NewOrderWasCreated,
      new NewOrderWasCreatedMessage
      {
        OrderId = orderId,
      },
      ct
    );
  }
}
