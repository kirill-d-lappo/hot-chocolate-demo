using System.Linq.Expressions;
using GreenDonut.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolateDemo.Models.Orders;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Services.OrderManagement.Orders;

public class OrderProviderService : IOrderProviderService
{
  private readonly IDbContextFactory<HCDemoDbContext> _dbContextFactory;

  public OrderProviderService(IDbContextFactory<HCDemoDbContext> dbContextFactory)
  {
    _dbContextFactory = dbContextFactory;
  }

  public async Task<Page<Order>> FindAllOrdersAsync(
    PagingArguments pageArgs,
    Expression<Func<Order, Order>> selector = null,
    IFilterContext filterContext = null,
    ISortingContext sortingContext = null,
    CancellationToken ct = default
  )
  {
    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    return await dbContext
      .Orders
      .AsNoTracking()
      .Select(OrderSelector)
      .WithSelection(selector)
      .OrderBy(u => u.Id)
      .OrderBySorting(sortingContext)
      .WhereFiltering(filterContext)
      .ToPageAsync(pageArgs, ct);
  }

  private static Expression<Func<OrderEntity, Order>> OrderSelector { get; } = o => new Order
  {
    Id = o.Id,
    UserId = o.UserId,
    OrderNumber = o.OrderNumber,
  };
}

public static class QueryableFilteringExtensions
{
  public static IQueryable<T> WhereFiltering<T>(this IQueryable<T> queryable, IFilterContext filter)
  {
    if (queryable is null)
    {
      throw new ArgumentNullException(nameof(queryable));
    }

    var predicate = filter?.AsPredicate<T>();

    return predicate is null
      ? queryable
      : queryable.Where(predicate);
  }

  public static IQueryable<T> OrderBySorting<T>(this IQueryable<T> queryable, ISortingContext sorting)
  {
    if (queryable is null)
    {
      throw new ArgumentNullException(nameof(queryable));
    }

    var predicate = sorting?.AsSortDefinition<T>();

    return predicate is null
      ? queryable
      : queryable.OrderBy(predicate);
  }
}
