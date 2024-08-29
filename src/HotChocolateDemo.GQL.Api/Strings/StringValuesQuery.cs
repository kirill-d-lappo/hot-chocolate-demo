namespace HotChocolateDemo.GQL.Api.Strings;

[QueryType]
public class StringValuesQuery
{
  private static readonly string[] StringValues =
  [
    "New-York",
    "Chicago",
    "Paris",
    null,
    "",
  ];

  private static readonly bool?[] BooleanValues =
  [
    true,
    false,
    null,
  ];

  [UsePaging]
  [UseFiltering]
  public IQueryable<string> GetAllStringValues()
  {
    return StringValues.AsQueryable();
  }

  [UsePaging]
  [UseFiltering]
  public IQueryable<bool?> GetAllBoolValues()
  {
    return BooleanValues.AsQueryable();
  }
}
