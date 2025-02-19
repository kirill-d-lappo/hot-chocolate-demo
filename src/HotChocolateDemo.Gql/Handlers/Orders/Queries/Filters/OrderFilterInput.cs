using HotChocolate.Data.Filters;
using HotChocolateDemo.Gql.Handlers.Filters;
using HotChocolateDemo.Models.Orders;

namespace HotChocolateDemo.Gql.Handlers.Orders.Queries.Filters;

public class OrderFilterInput : FilterInputType<Order>
{
  protected override void Configure(IFilterInputTypeDescriptor<Order> descriptor)
  {
    descriptor.BindFieldsExplicitly();
    descriptor
      .Field(x => x.Id)
      .Type<IdFilterInput>();

    descriptor
      .Field(x => x.OrderNumber)
      .Type<OrderNumberFilterInput>();

    descriptor
      .Field(x => x.UserId)
      .Type<IdFilterInput>();
  }
}
