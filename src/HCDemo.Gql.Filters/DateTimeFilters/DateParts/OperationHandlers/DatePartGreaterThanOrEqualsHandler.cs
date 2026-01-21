using System.Linq.Expressions;

namespace HCDemo.Gql.Filters.DateTimeFilters.DateParts.OperationHandlers;

/// <summary>
/// Handler for datePart "gte" (greater than or equal) operations.
/// </summary>
public class DatePartGreaterThanOrEqualsHandler : DatePartOperationHandlerBase
{
  protected override int Operation => DefaultFilterOperations.GreaterThanOrEquals;

  protected override bool IsNullAllowed => false;

  protected override Expression CreateComparisonExpression(Expression property, DateTime? value)
  {
    if (value is null)
    {
      return null;
    }

    return Expression.GreaterThanOrEqual(property, CreateParameterExpression(value.Value, property.Type));
  }
}
