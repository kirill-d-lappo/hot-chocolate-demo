using HCDemo.Models.Orders;
using HotChocolate.Data.Sorting;

namespace HCDemo.Gql.Handlers.Orders.Queries.Sorters;

public class OrderSortInput : SortInputType<Order>
{
  protected override void Configure(ISortInputTypeDescriptor<Order> descriptor)
  {
    descriptor.BindFieldsExplicitly();

    descriptor.Field(x => x.Id);
    descriptor.Field(x => x.OrderNumber);
  }
}
