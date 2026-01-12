using HotChocolate.Data.Sorting;
using HCDemo.Models.UserManagement;

namespace HCDemo.Gql.Handlers.Users.Queries.Sorters;

public class UserSortInput : SortInputType<User>
{
  protected override void Configure(ISortInputTypeDescriptor<User> descriptor)
  {
    descriptor.BindFieldsExplicitly();

    descriptor.Field(x => x.Id);
    descriptor.Field(x => x.UserName);
    descriptor.Field(x => x.BirthDateTime);
  }
}
