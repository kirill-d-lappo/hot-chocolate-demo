using System.Linq.Expressions;
using GreenDonut.Data;
using HotChocolateDemo.Models.Orders;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models.Orders;
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
      .Select(OrderSelector)
      .Where(r => ids.Contains(r.Id))
      .OrderBy(r => r.Id)
      .Select(b => b.Id, selectorBuilder)
      .ToDictionaryAsync(u => u.Id, ct);
  }

  private static Expression<Func<OrderEntity, Order>> OrderSelector { get; } = o => new Order
  {
    Id = o.Id,
    UserId = o.UserId,
    OrderNumber = o.OrderNumber,
    CreationSource = o.CreationSource,
  };
}
