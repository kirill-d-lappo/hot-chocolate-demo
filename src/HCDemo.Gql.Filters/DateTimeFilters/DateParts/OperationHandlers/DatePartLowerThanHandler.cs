using System.Linq.Expressions;
using HotChocolate.Data.Filters.Expressions;

namespace HCDemo.Gql.Filters.DateTimeFilters.DateParts.OperationHandlers;

/// <summary>
/// Handler for datePart "lt" (less than) operations.
/// </summary>
public class DatePartLowerThanHandler : DatePartOperationHandlerBase
{
  protected override int Operation => DefaultFilterOperations.LowerThan;

  protected override bool IsNullAllowed => false;

  protected override Expression CreateComparisonExpression(Expression property, DateTime? value)
  {
    if (value is null)
    {
      return null;
    }

    return FilterExpressionBuilder.LowerThan(property, value.Value);
  }
}
