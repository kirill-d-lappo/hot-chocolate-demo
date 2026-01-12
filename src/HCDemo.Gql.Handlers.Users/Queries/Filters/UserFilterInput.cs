using HCDemo.Gql.Filters;
using HotChocolate.Data.Filters;
using HCDemo.Models.UserManagement;

namespace HCDemo.Gql.Handlers.Users.Queries.Filters;

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

    descriptor.Field(o => o.BirthDateTime)

      // .Type<DateOnlyInDateTimeOffsetFilterInputType>();
      ;
  }
}
