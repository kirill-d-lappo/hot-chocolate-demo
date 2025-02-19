using HotChocolate.Data.Filters;

namespace HotChocolateDemo.Gql.Handlers.Filters;

public class IdFilterInput : LongOperationFilterInputType
{
  protected override void Configure(IFilterInputTypeDescriptor descriptor)
  {
    foreach (var operation in Operations())
    {
      descriptor
        .Operation(operation)
        .Type<LongType>();
    }
  }

  private static IEnumerable<int> Operations()
  {
    yield return DefaultFilterOperations.Equals;
    yield return DefaultFilterOperations.NotEquals;
    yield return DefaultFilterOperations.In;
    yield return DefaultFilterOperations.NotIn;
  }
}
