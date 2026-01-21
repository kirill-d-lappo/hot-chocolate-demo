using System.Linq.Expressions;

namespace HCDemo.Gql.Filters.DateTimeFilters.DateParts.OperationHandlers;

/// <summary>
/// Handler for datePart "lte" (less than or equal) operations.
/// </summary>
public class DatePartLowerThanOrEqualsHandler : DatePartOperationHandlerBase
{
  protected override int Operation => DefaultFilterOperations.LowerThanOrEquals;

  protected override bool IsNullAllowed => false;

  protected override Expression CreateComparisonExpression(Expression property, DateTime? value)
  {
    if (value is null)
    {
      return null;
    }

    return Expression.LessThanOrEqual(property, CreateParameterExpression(value.Value, property.Type));
  }
}
