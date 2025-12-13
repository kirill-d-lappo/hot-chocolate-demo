using HotChocolate.Data.Filters;

namespace HCDemo.Gql.Handlers.Users.Queries.Filters;

internal class UserNameFilterInput : StringOperationFilterInputType
{
  protected override void Configure(IFilterInputTypeDescriptor descriptor)
  {
    foreach (var operation in Operations())
    {
      descriptor
        .Operation(operation)
        .Type<StringType>();
    }
  }

  private static IEnumerable<int> Operations()
  {
    yield return DefaultFilterOperations.Equals;
    yield return DefaultFilterOperations.NotEquals;
    yield return DefaultFilterOperations.In;
    yield return DefaultFilterOperations.NotIn;
    yield return DefaultFilterOperations.StartsWith;
    yield return DefaultFilterOperations.NotStartsWith;
  }
}
