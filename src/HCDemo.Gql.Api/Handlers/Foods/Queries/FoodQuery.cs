using GreenDonut.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolate.Execution.Processing;
using HotChocolate.Types.Pagination;
using HCDemo.Models.Orders;
using HCDemo.Services.OrderManagement.Foods;

namespace HCDemo.Gql.Api.Handlers.Foods.Queries;

[QueryType]
public static class FoodQuery
{
  /// <summary>
  /// Searches for all foods in the system.
  /// </summary>
  [UsePaging]
  [UseProjection]
  [UseFiltering]
  [UseSorting]
  public static Task<Connection<Food>> AllFoods(
    PagingArguments pagingArgs,
    ISelection selection,
    IFilterContext filterContext,
    ISortingContext sortingContext,
    IFoodProviderService provider,
    CancellationToken ct
  )
  {
    return provider
      .FindAllFoodsAsync(pagingArgs, selection.AsSelector<Food>(), filterContext, sortingContext, ct)
      .ToConnectionAsync();
  }
}
