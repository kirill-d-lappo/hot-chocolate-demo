using HCDemo.Models.Orders;
using HCDemo.Services.OrderManagement.Orders;
using HCDemo.Services.OrderManagement.Orders.Events;

namespace HCDemo.Gql.Handlers.Orders.Subscriptions;

[SubscriptionType]
public static class OrderSubscriptions
{
  // Note [2024-12-12 klappo] we can inject into Streaming logic via With function
  // Note [2024-12-12 klappo] for example, to filter incoming events
  // public static async IAsyncEnumerable<NewOrderWasCreatedMessage> OnNewOrderWasCreatedStream(
  //   ITopicEventReceiver receiver,
  //   [EnumeratorCancellation] CancellationToken ct
  // )
  // {
  //   var eventStream = await receiver.SubscribeAsync<NewOrderWasCreatedMessage>(TopicNames.NewOrderWasCreated, ct);
  //
  //   await foreach (var message in eventStream
  //                    .ReadEventsAsync()
  //                    .WithCancellation(ct))
  //   {
  //     yield return message;
  //   }
  // }

  // [Subscribe(With = nameof(OnNewOrderWasCreatedStream))]
  [Subscribe]
  [Topic(TopicNames.NewOrderWasCreated)]
  public static async Task<Order> OnNewOrderWasCreated(
    [EventMessage] NewOrderWasCreatedMessage message,
    IFindOrderByIdDataLoader orderService,
    CancellationToken ct
  )
  {
    return await orderService.LoadAsync(message.OrderId, ct);
  }
}
