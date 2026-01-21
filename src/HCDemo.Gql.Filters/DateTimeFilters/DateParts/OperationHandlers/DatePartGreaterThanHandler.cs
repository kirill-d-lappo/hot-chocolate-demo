using System.Linq.Expressions;
using HotChocolate.Data.Filters.Expressions;

namespace HCDemo.Gql.Filters.DateTimeFilters.DateParts.OperationHandlers;

/// <summary>
/// Handler for datePart "gt" (greater than) operations.
/// </summary>
public class DatePartGreaterThanHandler : DatePartOperationHandlerBase
{
  protected override int Operation => DefaultFilterOperations.GreaterThan;

  protected override bool IsNullAllowed => false;

  protected override Expression CreateComparisonExpression(Expression property, DateTime? value)
  {
    if (value is null)
    {
      return null;
    }

    return FilterExpressionBuilder.GreaterThan(property, value.Value);
  }
}
