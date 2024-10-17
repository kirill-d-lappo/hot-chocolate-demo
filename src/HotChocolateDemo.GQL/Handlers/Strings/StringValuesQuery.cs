namespace HotChocolateDemo.GQL.Handlers.Strings;

[QueryType]
public class StringValuesQuery
{
  private static readonly string[] StringValues = ["New-York", "Chicago", "Paris", null, "",];

  private static readonly bool?[] BooleanValues = [true, false, null,];

  [UsePaging]
  [UseFiltering]
  public IEnumerable<string> AllStringValues()
  {
    return StringValues;
  }

  [UsePaging]
  [UseFiltering]
  public IEnumerable<bool?> AllBoolValues()
  {
    return BooleanValues;
  }
}
