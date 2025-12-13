namespace HCDemo.Services.OrderManagement.Orders;

public interface IOrderCreationService
{
  Task<long> CreateOrderAsync(CreateOrderParameters parameters, CancellationToken ct);
}
