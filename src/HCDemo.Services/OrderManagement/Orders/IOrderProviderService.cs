using System.Linq.Expressions;
using GreenDonut.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HCDemo.Models.Orders;

namespace HCDemo.Services.OrderManagement.Orders;

public interface IOrderProviderService
{
  Task<Page<Order>> FindAllOrdersAsync(
    PagingArguments pageArgs,
    Expression<Func<Order, Order>> selector = null,
    IFilterContext filterContext = null,
    ISortingContext sortingContext = null,
    CancellationToken ct = default
  );
}
