using System.Linq.Expressions;
using GreenDonut.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolateDemo.Models.Orders;

namespace HotChocolateDemo.Services.OrderManagement.Foods;

public interface IFoodProviderService
{
  Task<Page<Food>> FindAllFoodsAsync(
    PagingArguments pageArgs,
    Expression<Func<Food, Food>> selector = null,
    IFilterContext filterContext = null,
    ISortingContext sortingContext = null,
    CancellationToken ct = default
  );
}
