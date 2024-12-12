using HotChocolateDemo.Models.Orders;
using HotChocolateDemo.Models.UserManagement;
using HotChocolateDemo.Services.UserManagement.Users;

namespace HotChocolateDemo.GQL.Handlers.Orders.Queries;

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
}
