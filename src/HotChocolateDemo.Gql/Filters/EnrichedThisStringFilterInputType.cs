using HotChocolate.Data.Filters;

namespace HotChocolateDemo.Gql.Filters;

public class EnrichedThisStringFilterInputType : FilterInputType<string>
{
  protected override void Configure(IFilterInputTypeDescriptor<string> descriptor)
  {
    descriptor.Name("StringInputType");
    descriptor
      .ThisField<string>()
      .Type<StringOperationFilterInputType>();
  }
}
