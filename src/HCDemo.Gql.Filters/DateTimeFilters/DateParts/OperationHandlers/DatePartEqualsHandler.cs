using System.Linq.Expressions;
using HotChocolate.Data.Filters.Expressions;

namespace HCDemo.Gql.Filters.DateTimeFilters.DateParts.OperationHandlers;

/// <summary>
/// Handler for datePart "eq" (equals) operations.
/// </summary>
public class DatePartEqualsHandler : DatePartOperationHandlerBase
{
  protected override int Operation => DefaultFilterOperations.Equals;

  protected override Expression CreateComparisonExpression(Expression property, DateTime? value)
  {
    if (value is null)
    {
      return Expression.Equal(property, Expression.Constant(null, typeof(DateTime?)));
    }

    return FilterExpressionBuilder.Equals(property, value.Value);
  }
}
