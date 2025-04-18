using System.Linq.Expressions;
using GreenDonut.Data;
using HotChocolateDemo.Models.Orders;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Services.OrderManagement.Foods;

internal static class FoodDataLoader
{
  [DataLoader]
  public static async Task<Dictionary<long, Food>> FindFoodByIdAsync(
    IReadOnlyList<long> ids,
    ISelectorBuilder selectorBuilder,
    IDbContextFactory<HCDemoDbContext> contextFactory,
    CancellationToken ct
  )
  {
    await using var context = await contextFactory.CreateDbContextAsync(ct);

    return await context
      .Foods
      .AsNoTracking()
      .OrderBy(r => r.Id)
      .Where(r => ids.Contains(r.Id))
      .Select(FoodSelector)
      .Select(b => b.Id, selectorBuilder)
      .ToDictionaryAsync(u => u.Id, ct);
  }

  private static Expression<Func<FoodEntity, Food>> FoodSelector { get; } = u => new Food
  {
    Id = u.Id,
    Name = u.Name,
  };
}
