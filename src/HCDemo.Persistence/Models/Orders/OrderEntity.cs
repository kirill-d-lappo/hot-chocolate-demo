using HCDemo.Models.Orders;
using HCDemo.Persistence.Models.UserManagement;

namespace HCDemo.Persistence.Models.Orders;

public class OrderEntity
{
  [Key]
  public long Id { get; set; }

  [MaxLength(64)]
  public string OrderNumber { get; set; }

  [Column(TypeName = "nvarchar(16)")]
  public OrderCreationSource CreationSource { get; set; }

  public long? UserId { get; set; }

  public UserEntity User { get; set; }

  public ICollection<FoodOrderItemEntity> FoodOrderItems { get; set; }
}
