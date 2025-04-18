using FluentValidation;
using HotChocolate.Subscriptions;
using HotChocolateDemo.Models.Orders;
using HotChocolateDemo.Services.OrderManagement.Orders;

namespace HotChocolateDemo.Gql.Handlers.Orders.Mutations;

[MutationType]
public class OrderMutations
{
  [Error<ValidationException>]
  public async Task<Order> CreateOrder(
    [GraphQLNonNullType] CreateOrderInput input,
    IOrderCreationService creationService,
    IFindOrderByIdDataLoader dataLoader,
    ITopicEventSender topicEventSender,
    CancellationToken ct
  )
  {
    var createParams = ToCreateOrderParams(input);

    var orderId = await creationService.CreateOrderAsync(createParams, ct);

    await topicEventSender.SendOrderCreatedAsync(orderId, ct);

    return await dataLoader.LoadAsync(orderId, ct);
  }

  private static CreateOrderParameters ToCreateOrderParams(CreateOrderInput input)
  {
    return new CreateOrderParameters
    {
      UserId = input.UserId,
      FoodIds = input.FoodIds,
      CreationSource = OrderCreationSource.FrontEnd,
    };
  }
}
