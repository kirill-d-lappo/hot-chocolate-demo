using HotChocolate.Data.Sorting;
using HotChocolateDemo.Models.Orders;
using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.Gql.Handlers.Users.Queries.Sorters;

public class UserSortInput : SortInputType<User>
{
  protected override void Configure(ISortInputTypeDescriptor<User> descriptor)
  {
    descriptor.BindFieldsExplicitly();

    descriptor.Field(x => x.Id);
    descriptor.Field(x => x.UserName);
  }
}
