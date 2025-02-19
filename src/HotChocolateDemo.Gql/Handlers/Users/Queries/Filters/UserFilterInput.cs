using HotChocolate.Data.Filters;
using HotChocolateDemo.Gql.Handlers.Filters;
using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.Gql.Handlers.Users.Queries.Filters;

public class UserFilterInput : FilterInputType<User>
{
  protected override void Configure(IFilterInputTypeDescriptor<User> descriptor)
  {
    descriptor.BindFieldsExplicitly();

    descriptor
      .Field(o => o.Id)
      .Type<IdFilterInput>();

    descriptor
      .Field(o => o.UserName)
      .Type<UserNameFilterInput>();

    descriptor
      .Field(o => o.BirthDateTime)
      .Type<UserBirthdayFilterInput>();
  }
}
