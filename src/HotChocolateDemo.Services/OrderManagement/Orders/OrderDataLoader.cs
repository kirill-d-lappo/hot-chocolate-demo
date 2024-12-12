using GreenDonut.Selectors;
using HotChocolateDemo.Models.Orders;
using HotChocolateDemo.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Services.OrderManagement.Orders;

internal static class OrderDataLoader
{
  [DataLoader]
  public static async Task<Dictionary<long, Order>> FindOrderByIdAsync(
    IReadOnlyList<long> ids,
    ISelectorBuilder selectorBuilder,
    IDbContextFactory<HCDemoDbContext> contextFactory,
    CancellationToken ct
  )
  {
    await using var context = await contextFactory.CreateDbContextAsync(ct);

    return await context
      .Orders
      .AsNoTracking()
      .OrderBy(r => r.Id)
      .Where(r => ids.Contains(r.Id))
      .Select(b => b.Id, selectorBuilder)
      .ToDictionaryAsync(u => u.Id, ct);
  }
}
