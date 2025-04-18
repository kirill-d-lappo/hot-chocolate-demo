using System.Linq.Expressions;
using GreenDonut.Data;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolateDemo.Models.Orders;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Services.OrderManagement.Foods;

public class FoodProviderService : IFoodProviderService
{
  private readonly IDbContextFactory<HCDemoDbContext> _dbContextFactory;

  public FoodProviderService(IDbContextFactory<HCDemoDbContext> dbContextFactory)
  {
    _dbContextFactory = dbContextFactory;
  }

  public async Task<Page<Food>> FindAllFoodsAsync(
    PagingArguments pageArgs,
    Expression<Func<Food, Food>> selector = null,
    IFilterContext filterContext = null,
    ISortingContext sortingContext = null,
    CancellationToken ct = default
  )
  {
    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    return await dbContext
      .Foods
      .AsNoTracking()
      .Select(FoodSelector)
      .SelectSelection(selector)
      .OrderBy(u => u.Id)
      .OrderBySorting(sortingContext)
      .WhereFiltering(filterContext)
      .ToPageAsync(pageArgs, ct);
  }

  private static Expression<Func<FoodEntity, Food>> FoodSelector { get; } = o => new Food
  {
    Id = o.Id,
    Name = o.Name,
  };
}
