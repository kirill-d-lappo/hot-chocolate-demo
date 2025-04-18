﻿using HotChocolateDemo.Models.Orders;

namespace HotChocolateDemo.Services.OrderManagement.Orders;

public class CreateOrderParameters
{
  public long UserId { get; set; }

  public OrderCreationSource CreationSource { get; set; }

  public List<long> FoodIds { get; set; }
}
