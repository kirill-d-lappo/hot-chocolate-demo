using HotChocolate.Data.Filters;

namespace HotChocolateDemo.Gql.Handlers.Filters;

public class IdFilterInput : LongOperationFilterInputType
{
  protected override void Configure(IFilterInputTypeDescriptor descriptor)
  {
    var collectionOfLongType = typeof(ListType<>).MakeGenericType(typeof(LongType));
    var longType = typeof(LongType);

    descriptor
      .Operation(DefaultFilterOperations.Equals)
      .Type(longType);

    descriptor
      .Operation(DefaultFilterOperations.NotEquals)
      .Type(longType);

    descriptor
      .Operation(DefaultFilterOperations.In)
      .Type(collectionOfLongType);

    descriptor
      .Operation(DefaultFilterOperations.NotIn)
      .Type(collectionOfLongType);
  }
}
