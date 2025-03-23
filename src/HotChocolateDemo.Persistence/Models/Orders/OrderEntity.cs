using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotChocolateDemo.Models.Orders;
using HotChocolateDemo.Persistence.Models.UserManagement;

namespace HotChocolateDemo.Persistence.Models.Orders;

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
