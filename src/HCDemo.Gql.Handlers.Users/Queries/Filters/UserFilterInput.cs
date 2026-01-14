using HCDemo.Gql.Filters;
using HotChocolate.Data.Filters;
using HCDemo.Models.UserManagement;
using HCDemo.Persistence.Models.UserManagement;

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

    // BirthDateTime automatically uses DateTimeExtendedOperationFilterInputType
    // due to global binding via AddDateTimeExtendedFiltering()
    descriptor
      .Field(o => o.BirthDateTime);
  }
}

// FixMe [2026-01-14 klappo] remove it after resolving issue with layering in UserQuery
public class UserEntityFilterInput : FilterInputType<UserEntity>
{
  protected override void Configure(IFilterInputTypeDescriptor<UserEntity> descriptor)
  {
    descriptor.BindFieldsExplicitly();

    descriptor
      .Field(o => o.Id)
      .Type<IdFilterInput>();

    descriptor
      .Field(o => o.UserName)
      .Type<UserNameFilterInput>();

    // BirthDateTime automatically uses DateTimeExtendedOperationFilterInputType
    // due to global binding via AddDateTimeExtendedFiltering()
    descriptor
      .Field(o => o.BirthDateTime);
  }
}
