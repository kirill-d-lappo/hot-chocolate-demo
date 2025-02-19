using HotChocolate.Subscriptions;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models.Orders;
using HotChocolateDemo.Services.OrderManagement.Orders.Errors;
using HotChocolateDemo.Services.OrderManagement.Orders.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotChocolateDemo.Services.OrderManagement.Orders;

public class OrderCreationService : IOrderCreationService
{
  private readonly IDbContextFactory<HCDemoDbContext> _dbContextFactory;
  private readonly ILogger<OrderCreationService> _logger;
  private readonly ITopicEventSender _topicEventSender;

  public OrderCreationService(
    IDbContextFactory<HCDemoDbContext> dbContextFactory,
    ILogger<OrderCreationService> logger,
    ITopicEventSender topicEventSender
  )
  {
    _dbContextFactory = dbContextFactory;
    _logger = logger;
    _topicEventSender = topicEventSender;
  }

  public async Task<long> CreateOrderAsync(CreateOrderParameters parameters, CancellationToken ct)
  {
    var order = await CreateOrder(parameters, ct);

    await _topicEventSender.SendAsync(
      TopicNames.NewOrderWasCreated,
      new NewOrderWasCreatedMessage
      {
        OrderId = order.Id,
        OrderNumber = order.OrderNumber,
      },
      ct
    );

    _logger.OrderWasCreated(order);

    return order.Id;
  }

  private async Task<OrderEntity> CreateOrder(CreateOrderParameters parameters, CancellationToken ct)
  {
    var order = new OrderEntity
    {
      OrderNumber = Guid
        .NewGuid()
        .ToString("N"),
      UserId = parameters.UserId,
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
