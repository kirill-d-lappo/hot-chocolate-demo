using GreenDonut.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolate.Execution.Processing;
using HotChocolate.Types.Pagination;
using HotChocolateDemo.Models.Orders;
using HotChocolateDemo.Services.OrderManagement.Orders;

namespace HotChocolateDemo.Gql.Handlers.Orders.Queries;

[QueryType]
public static class OrderQuery
{
  /// <summary>
  /// Searches for all orders in the system.
  /// </summary>
  [UsePaging]
  [UseFiltering]
  [UseSorting]
  public static Task<Connection<Order>> AllOrders(
    PagingArguments pagingArgs,
    ISelection selection,
    IFilterContext filterContext,
    ISortingContext sortingContext,
    IOrderProviderService provider,
    CancellationToken ct
  )
  {
    return provider
      .FindAllOrdersAsync(pagingArgs, selection.AsSelector<Order>(), filterContext, sortingContext, ct)
      .ToConnectionAsync();
  }
}
