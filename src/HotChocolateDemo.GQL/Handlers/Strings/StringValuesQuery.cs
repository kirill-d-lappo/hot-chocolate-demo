namespace HotChocolateDemo.GQL.Handlers.Strings;

[QueryType]
public class StringValuesQuery
{
  private static readonly string[] StringValues = ["New-York", "Chicago", "Paris", null, "",];

  private static readonly bool?[] BooleanValues = [true, false, null,];

  [UsePaging]
  [UseFiltering]
  public IQueryable<string> AllStringValues()
  {
    return StringValues.AsQueryable();
  }

  [UsePaging]
  [UseFiltering]
  public IQueryable<bool?> AllBoolValues()
  {
    return BooleanValues.AsQueryable();
  }
}
