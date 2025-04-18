using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models.Orders;
using HotChocolateDemo.Services.OrderManagement.Orders.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotChocolateDemo.Services.OrderManagement.Orders;

public class OrderCreationService : IOrderCreationService
{
  private readonly IDbContextFactory<HCDemoDbContext> _dbContextFactory;
  private readonly ILogger<OrderCreationService> _logger;

  public OrderCreationService(IDbContextFactory<HCDemoDbContext> dbContextFactory, ILogger<OrderCreationService> logger)
  {
    _dbContextFactory = dbContextFactory;
    _logger = logger;
  }

  public async Task<long> CreateOrderAsync(CreateOrderParameters parameters, CancellationToken ct)
  {
    var order = await CreateOrder(parameters, ct);
    var orderId = order.Id;

    _logger.OrderWasCreated(orderId);

    return orderId;
  }

  private async Task<OrderEntity> CreateOrder(CreateOrderParameters parameters, CancellationToken ct)
  {
    var order = new OrderEntity
    {
      OrderNumber = Guid
        .NewGuid()
        .ToString("N"),
      UserId = parameters.UserId,
      CreationSource = parameters.CreationSource,
      FoodOrderItems = parameters
        .FoodIds
        ?.Select(fid => new FoodOrderItemEntity
          {
            FoodId = fid,
          }
        )
        .ToList(),
    };

    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);
    await dbContext.Orders.AddAsync(order, ct);

    try
    {
      await dbContext.SaveChangesAsync(ct);
    }
    catch (DbUpdateException e)
    {
      throw new OrderCreationException(e);
    }

    return order;
  }
}
