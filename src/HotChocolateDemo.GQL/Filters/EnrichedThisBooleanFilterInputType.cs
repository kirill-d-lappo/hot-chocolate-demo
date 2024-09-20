using HotChocolate.Data.Filters;

namespace HotChocolateDemo.GQL.Filters;

public class EnrichedThisBooleanFilterInputType : FilterInputType<bool>
{
  protected override void Configure(IFilterInputTypeDescriptor<bool> descriptor)
  {
    descriptor.Name("BooleanInputType");
    descriptor
     .ThisField<bool>()
     .Type<BooleanOperationFilterInputType>();
  }
}
