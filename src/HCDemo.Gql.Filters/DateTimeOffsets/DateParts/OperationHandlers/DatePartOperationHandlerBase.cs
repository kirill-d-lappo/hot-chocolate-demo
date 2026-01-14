using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using HotChocolate.Configuration;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;

namespace HCDemo.Gql.Filters.DateTimeOffsets.DateParts.OperationHandlers;

/// <summary>
/// Base class for datePart operation handlers that properly converts DateTimeOffset to DateTime.
///
/// HotChocolate's DateTime scalar internally parses values to DateTimeOffset.
/// This base class intercepts the parsed value and converts it to DateTime
/// before creating the comparison expression.
/// </summary>
public abstract class DatePartOperationHandlerBase : FilterOperationHandler<QueryableFilterContext, Expression>
{
  /// <summary>
  /// The filter operation ID this handler processes (eq, neq, gt, etc.)
  /// </summary>
  protected abstract int Operation { get; }

  /// <summary>
  /// Determines if this handler can process the given field.
  /// Only handles operations on DatePartOperationFilterInputType.
  /// </summary>
  public override bool CanHandle(
    ITypeCompletionContext context,
    IFilterInputTypeDefinition typeDefinition,
    IFilterFieldDefinition fieldDefinition
  )
  {
    // Only handle if the parent type is our DatePartOperationFilterInputType
    // and this is the operation we care about
    return context.Type is DatePartOperationFilterInputType
      && fieldDefinition is FilterOperationFieldDefinition operationField
      && operationField.Id == Operation;
  }

  public override bool TryHandleOperation(
    QueryableFilterContext context,
    IFilterOperationField field,
    ObjectFieldNode node,
    [NotNullWhen(true)] out Expression result
  )
  {
    // Get the current expression (should be DateTime from .Date property)
    var property = context.GetInstance();

    // Get the parsed value - HotChocolate parses DateTime input as DateTimeOffset
    var value = node.Value;
    var parsedValue = ParseInputValue(value, field);

    // Convert DateTimeOffset to DateTime if needed
    var convertedValue = ConvertToDateTime(parsedValue);

    if (convertedValue is null && !IsNullAllowed)
    {
      result = null;

      return false;
    }

    // Create the comparison expression
    result = CreateComparisonExpression(property, convertedValue);

    return result is not null;
  }

  /// <summary>
  /// Whether null values are allowed for this operation.
  /// </summary>
  protected virtual bool IsNullAllowed => true;

  /// <summary>
  /// Parses the input value node to a CLR object.
  /// </summary>
  protected virtual object ParseInputValue(IValueNode value, IFilterOperationField field)
  {
    if (value is NullValueNode)
    {
      return null;
    }

    // Use the field's input type to parse the literal
    var namedType = field.Type.NamedType();
    if (namedType is ILeafType leafType)
    {
      return leafType.ParseLiteral(value);
    }

    // Fallback: try to parse as string
    if (value is StringValueNode stringNode)
    {
      return stringNode.Value;
    }

    return null;
  }

  /// <summary>
  /// Converts the parsed value to DateTime.
  /// Handles DateTimeOffset, DateTime, and null values.
  /// </summary>
  protected static DateTime? ConvertToDateTime(object parsedValue)
  {
    return parsedValue switch
    {
      null                                           => null,
      DateTime dt                                    => dt,
      DateTimeOffset dto                             => dto.DateTime,
      DateOnly d                                     => d.ToDateTime(TimeOnly.MinValue),
      string s when DateTime.TryParse(s, out var dt) => dt,
      string s when DateTime.TryParseExact(
        s,
        "yyyy-MM-dd",
        CultureInfo.InvariantCulture,
        DateTimeStyles.None,
        out var dt
      ) => dt,
      string s when DateTimeOffset.TryParse(s, out var dto) => dto.DateTime,
      _ => throw new InvalidOperationException(
        $"Cannot convert value of type '{parsedValue.GetType().Name}' to DateTime"
      ),
    };
  }

  /// <summary>
  /// Converts a list of values to DateTime array.
  /// </summary>
  protected static DateTime[] ConvertToDateTimeArray(object parsedValue)
  {
    if (parsedValue is null)
    {
      return null;
    }

    if (parsedValue is not System.Collections.IEnumerable enumerable)
    {
      throw new InvalidOperationException($"Expected enumerable but got '{parsedValue.GetType().Name}'");
    }

    var result = new List<DateTime>();
    foreach (var item in enumerable)
    {
      var converted = ConvertToDateTime(item);
      if (converted.HasValue)
      {
        result.Add(converted.Value);
      }
    }

    return result.ToArray();
  }

  /// <summary>
  /// Creates a constant/parameter expression for a value with proper type handling.
  /// This is used for comparison operations that need to create Expression trees.
  /// </summary>
  protected static Expression CreateParameterExpression(object value, Type targetType)
  {
    // If target type is nullable, we need to convert value to nullable
    var underlyingType = Nullable.GetUnderlyingType(targetType);
    if (underlyingType is not null)
    {
      // Convert value to nullable type
      var convertedValue = Convert.ChangeType(value, underlyingType);

      return Expression.Constant(convertedValue, targetType);
    }

    return Expression.Constant(value, targetType);
  }

  /// <summary>
  /// Creates the comparison expression for this operation.
  /// </summary>
  protected abstract Expression CreateComparisonExpression(Expression property, DateTime? value);
}
