using GreenDonut.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolate.Execution.Processing;
using HotChocolate.Types.Pagination;
using HCDemo.Models.Orders;
using HCDemo.Services.OrderManagement.Orders;

namespace HCDemo.Gql.Api.Handlers.Orders.Queries;

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
