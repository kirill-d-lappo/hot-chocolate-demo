using System.Linq.Expressions;
using GreenDonut.Data;
using HCDemo.Models.Orders;
using HCDemo.Persistence;
using HCDemo.Persistence.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace HCDemo.Services.OrderManagement.Foods;

internal static class FoodOrderDataLoader
{
  [DataLoader]
  public static async Task<Dictionary<long, FoodOrderItem>> FindFoodOrderItemByIdAsync(
    IReadOnlyList<long> ids,
    ISelectorBuilder selectorBuilder,
    IDbContextFactory<HCDemoDbContext> contextFactory,
    CancellationToken ct
  )
  {
    await using var context = await contextFactory.CreateDbContextAsync(ct);

    return await context
      .FoodOrderItems
      .AsNoTracking()
      .OrderBy(r => r.Id)
      .Where(r => ids.Contains(r.Id))
      .Select(FoodOrderItemSelector)
      .Select(b => b.Id, selectorBuilder)
      .ToDictionaryAsync(u => u.Id, ct);
  }

  [DataLoader]
  public static async Task<ILookup<long, FoodOrderItem>> FindAllFoodOrderItemsByOrderIdAsync(
    IReadOnlyList<long> orderIds,
    ISelectorBuilder selectorBuilder,
    IDbContextFactory<HCDemoDbContext> contextFactory,
    CancellationToken ct
  )
  {
    await using var context = await contextFactory.CreateDbContextAsync(ct);

    return context
      .FoodOrderItems
      .AsNoTracking()
      .Where(r => orderIds.Contains(r.OrderId))
      .OrderBy(r => r.Id)
      .Select(FoodOrderItemSelector)
      .Select(b => b.Id, selectorBuilder)
      .ToLookup(u => u.OrderId);
  }

  private static Expression<Func<FoodOrderItemEntity, FoodOrderItem>> FoodOrderItemSelector { get; } = u =>
    new FoodOrderItem
    {
      Id = u.Id,
      FoodId = u.FoodId,
      OrderId = u.OrderId,
      Count = u.Count,
    };
}
