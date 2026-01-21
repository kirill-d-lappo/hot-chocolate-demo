using System.Linq.Expressions;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;

namespace HCDemo.Gql.Filters.DateTimeFilters.DateParts.OperationHandlers;

/// <summary>
/// Handler for datePart "nin" (not in) operations.
/// </summary>
public class DatePartNotInHandler : DatePartOperationHandlerBase
{
  protected override int Operation => DefaultFilterOperations.NotIn;

  protected override Expression CreateComparisonExpression(Expression property, DateTime? value)
  {
    // "nin" uses array, need special handling
    throw new NotSupportedException("Use TryHandleOperation instead");
  }

  public override bool TryHandleOperation(
    QueryableFilterContext context,
    IFilterOperationField field,
    ObjectFieldNode node,
    [System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out Expression result
  )
  {
    var property = context.GetInstance();
    var value = node.Value;
    var parsedValue = ParseListInputValue(value);

    var convertedValues = ConvertToDateTimeArray(parsedValue);

    if (convertedValues is null || convertedValues.Length == 0)
    {
      result = null;

      return false;
    }

    // Create NOT(In(...)) expression
    var inExpression = FilterExpressionBuilder.In(property, typeof(DateTime), convertedValues);
    result = Expression.Not(inExpression);

    return true;
  }

  /// <summary>
  /// Parses a list value node to an array of strings.
  /// The 'nin' operation receives a ListValueNode containing StringValueNodes.
  /// </summary>
  private static string[] ParseListInputValue(IValueNode value)
  {
    if (value is NullValueNode)
    {
      return null;
    }

    if (value is not ListValueNode listNode)
    {
      throw new InvalidOperationException(
        $"Expected ListValueNode for 'nin' operation but got '{value.GetType().Name}'"
      );
    }

    var result = new List<string>();
    foreach (var item in listNode.Items)
    {
      if (item is StringValueNode stringNode)
      {
        result.Add(stringNode.Value);
      }
      else if (item is NullValueNode)
      {
        // Skip null values in the list
        continue;
      }
      else
      {
        throw new InvalidOperationException(
          $"Expected StringValueNode in list but got '{item.GetType().Name}'"
        );
      }
    }

    return result.ToArray();
  }
}
