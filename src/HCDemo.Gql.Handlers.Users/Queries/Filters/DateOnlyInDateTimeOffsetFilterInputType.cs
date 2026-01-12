using HotChocolate.Data.Filters;

namespace HCDemo.Gql.Handlers.Users.Queries.Filters;

public class DateOnlyInDateTimeOffsetFilterInputType : ComparableOperationFilterInputType<DateTimeOffset>
{
  protected override void Configure(IFilterInputTypeDescriptor descriptor)
  {
    base.Configure(descriptor);

    descriptor.AllowOr();
    descriptor.AllowAnd();

    descriptor
      .Operation(DefaultFilterOperations.Equals)
      .Type<DateTimeType>();

    descriptor
      .Operation(DefaultFilterOperations.NotEquals)
      .Type<DateTimeType>();

    descriptor
      .Operation(DefaultFilterOperations.GreaterThan)
      .Type<DateTimeType>();

    descriptor
      .Operation(DefaultFilterOperations.GreaterThanOrEquals)
      .Type<DateTimeType>();

    descriptor
      .Operation(DefaultFilterOperations.LowerThan)
      .Type<DateTimeType>();

    descriptor
      .Operation(DefaultFilterOperations.LowerThanOrEquals)
      .Type<DateTimeType>();

    descriptor
      .Operation(DefaultFilterOperations.In)
      .Type<ListType<DateTimeType>>();

    descriptor
      .Operation(DefaultFilterOperations.NotIn)
      .Type<ListType<DateTimeType>>();
  }
}
