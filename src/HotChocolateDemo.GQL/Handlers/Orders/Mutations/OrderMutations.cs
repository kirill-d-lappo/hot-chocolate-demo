using FluentValidation;
using HotChocolateDemo.Models.Orders;
using HotChocolateDemo.Services.OrderManagement.Orders;

namespace HotChocolateDemo.GQL.Handlers.Orders.Mutations;

[MutationType]
public class OrderMutations
{
  [Error<ValidationException>]
  public async Task<Order> CreateOrder(
    [GraphQLNonNullType] CreateOrderInput input,
    IOrderCreationService creationService,
    IFindOrderByIdDataLoader dataLoader,
    CancellationToken ct
  )
  {
    var createParams = ToCreateOrderParams(input);

    var orderId = await creationService.CreateOrderAsync(createParams, ct);

    return await dataLoader.LoadAsync(orderId, ct);
  }

  private static CreateOrderParameters ToCreateOrderParams(CreateOrderInput input)
  {
    return new CreateOrderParameters
    {
      UserId = input.UserId,
    };
  }
}
