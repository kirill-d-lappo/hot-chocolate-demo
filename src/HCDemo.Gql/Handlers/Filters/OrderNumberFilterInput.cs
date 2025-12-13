using HotChocolate.Data.Filters;

namespace HCDemo.Gql.Handlers.Filters;

internal class OrderNumberFilterInput : StringOperationFilterInputType
{
  protected override void Configure(IFilterInputTypeDescriptor descriptor)
  {
    var collectionOfStringType = typeof(ListType<>).MakeGenericType(typeof(StringType));
    var stringType = typeof(StringType);

    descriptor
      .Operation(DefaultFilterOperations.Equals)
      .Type(stringType);

    descriptor
      .Operation(DefaultFilterOperations.NotEquals)
      .Type(stringType);

    descriptor
      .Operation(DefaultFilterOperations.In)
      .Type(collectionOfStringType);

    descriptor
      .Operation(DefaultFilterOperations.NotIn)
      .Type(collectionOfStringType);

    descriptor
      .Operation(DefaultFilterOperations.StartsWith)
      .Type(stringType);

    descriptor
      .Operation(DefaultFilterOperations.NotStartsWith)
      .Type(stringType);
  }
}
