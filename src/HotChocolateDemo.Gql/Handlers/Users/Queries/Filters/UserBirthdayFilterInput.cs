using HotChocolate.Data.Filters;

namespace HotChocolateDemo.Gql.Handlers.Users.Queries.Filters;

public class UserBirthdayFilterInput : DateTimeOperationFilterInputType
{
  protected override void Configure(IFilterInputTypeDescriptor descriptor)
  {
    foreach (var operation in Operations())
    {
      descriptor
        .Operation(operation)
        .Type<DateTimeType>();
    }
  }

  private static IEnumerable<int> Operations()
  {
    yield return DefaultFilterOperations.Equals;
    yield return DefaultFilterOperations.NotEquals;
    yield return DefaultFilterOperations.GreaterThan;
    yield return DefaultFilterOperations.GreaterThanOrEquals;
    yield return DefaultFilterOperations.NotGreaterThan;
    yield return DefaultFilterOperations.NotGreaterThanOrEquals;
    yield return DefaultFilterOperations.LowerThan;
    yield return DefaultFilterOperations.LowerThanOrEquals;
    yield return DefaultFilterOperations.NotLowerThan;
    yield return DefaultFilterOperations.NotLowerThanOrEquals;
  }
}
