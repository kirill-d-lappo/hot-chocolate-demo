using HCDemo.Models.Orders;
using HCDemo.Models.UserManagement;
using HCDemo.Services.OrderManagement.Foods;
using HCDemo.Services.UserManagement.Users;

namespace HCDemo.Gql.Handlers.Orders.Queries;

[ExtendObjectType<Order>]
public static class OrderQueryExtensions
{
  public static async Task<User> User([Parent] Order order, IFindUserByIdDataLoader dataLoader)
  {
    if (!order.UserId.HasValue)
    {
      return null;
    }

    return await dataLoader.LoadAsync(order.UserId.Value);
  }

  public static async Task<IEnumerable<FoodOrderItem>> FoodOrderItems(
    [Parent] Order order,
    IFindAllFoodOrderItemsByOrderIdDataLoader dataLoader
  )
  {
    if (order is not { Id: > 0, })
    {
      return null;
    }

    return await dataLoader.LoadAsync(order.Id);
  }
}
