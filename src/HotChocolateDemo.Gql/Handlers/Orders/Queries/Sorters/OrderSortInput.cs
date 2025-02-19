using HotChocolate.Data.Sorting;
using HotChocolateDemo.Models.Orders;

namespace HotChocolateDemo.Gql.Handlers.Orders.Queries.Sorters;

public class OrderSortInput : SortInputType<Order>
{
  protected override void Configure(ISortInputTypeDescriptor<Order> descriptor)
  {
    descriptor.BindFieldsExplicitly();

    descriptor.Field(x => x.Id);
    descriptor.Field(x => x.OrderNumber);
  }
}
