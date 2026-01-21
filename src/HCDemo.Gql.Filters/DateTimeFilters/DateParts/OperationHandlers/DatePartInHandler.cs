using System.Linq.Expressions;
using HotChocolate.Data.Filters.Expressions;

namespace HCDemo.Gql.Filters.DateTimeFilters.DateParts.OperationHandlers;

/// <summary>
/// Handler for datePart "in" operations.
/// </summary>
public class DatePartInHandler : DatePartOperationHandlerBase
{
  protected override int Operation => DefaultFilterOperations.In;

  protected override Expression CreateComparisonExpression(Expression property, DateTime? value)
  {
    // "in" uses array, need special handling
    throw new NotSupportedException("Use TryHandleOperation instead");
  }

  public override bool TryHandleOperation(
    QueryableFilterContext context,
    IFilterOperationField field,
    HotChocolate.Language.ObjectFieldNode node,
    [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out Expression result
  )
  {
    var property = context.GetInstance();
    var value = node.Value;
    var parsedValue = ParseInputValue(value, field);

    var convertedValues = ConvertToDateTimeArray(parsedValue);

    if (convertedValues is null || convertedValues.Length == 0)
    {
      result = null;

      return false;
    }

    result = FilterExpressionBuilder.In(property, typeof(DateTime), convertedValues);

    return true;
  }
}
