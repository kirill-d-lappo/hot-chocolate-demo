using System.Linq.Expressions;
using HotChocolate.Data.Filters.Expressions;

namespace HCDemo.Gql.Filters.DateTimeFilters.DateParts.OperationHandlers;

/// <summary>
/// Handler for datePart "neq" (not equals) operations.
/// </summary>
public class DatePartNotEqualsHandler : DatePartOperationHandlerBase
{
  protected override int Operation => DefaultFilterOperations.NotEquals;

  protected override Expression CreateComparisonExpression(Expression property, DateTime? value)
  {
    if (value is null)
    {
      return Expression.NotEqual(property, Expression.Constant(null, typeof(DateTime?)));
    }

    return FilterExpressionBuilder.NotEquals(property, value.Value);
  }
}
