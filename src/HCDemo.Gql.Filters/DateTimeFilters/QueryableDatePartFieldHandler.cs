using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using HotChocolate.Configuration;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;
using HotChocolate.Language.Visitors;

namespace HCDemo.Gql.Filters.DateTimeFilters;

/// <summary>
/// Field handler that processes the "datePart" field on DateTimeOffset filters.
/// It translates DateOnly comparisons to DateTimeOffset.Date comparisons in EF Core expressions.
///
/// This handler matches when the field name is "datePart".
/// It transforms the property expression from DateTimeOffset to its Date property,
/// allowing date-only comparisons that translate to SQL CONVERT(date, ...) operations.
///
/// SQL Translation example:
/// Input:  { birthDateTime: { datePart: { eq: "2000-01-15" } } }
/// SQL:    WHERE CONVERT(date, BirthDateTime) = '2000-01-15'
/// </summary>
public class QueryableDatePartFieldHandler : FilterFieldHandler<QueryableFilterContext, Expression>
{
  /// <summary>
  /// Determines if this handler can process the given field.
  /// Returns true for "datePart" fields.
  /// </summary>
  public override bool CanHandle(
    ITypeCompletionContext context,
    IFilterInputTypeDefinition typeDefinition,
    IFilterFieldDefinition fieldDefinition
  )
  {
    // Check if the field is our custom datePart field
    return fieldDefinition.Name == DateTimeOffsetFilterFields.DatePart;
  }

  public override bool TryHandleEnter(
    QueryableFilterContext context,
    IFilterField field,
    ObjectFieldNode node,
    [NotNullWhen(true)] out ISyntaxVisitorAction action
  )
  {
    if (node.Value.IsNull())
    {
      action = SyntaxVisitor.Skip;

      return true;
    }

    // The current instance should be DateTimeOffset or DateTimeOffset?
    var property = context.GetInstance();

    // Convert to .Date property access
    // For DateTimeOffset: x.BirthDateTime.Date
    // For DateTimeOffset?: x.BirthDateTime.Value.Date
    Expression dateExpression;
    if (property.Type == typeof(DateTimeOffset))
    {
      dateExpression = Expression.Property(property, nameof(DateTimeOffset.Date));
    }
    else if (property.Type == typeof(DateTimeOffset?))
    {
      // For nullable, access .Value.Date
      // This is safe because EF Core handles null checking at SQL level
      var valueProperty = Expression.Property(property, nameof(Nullable<DateTimeOffset>.Value));
      dateExpression = Expression.Property(valueProperty, nameof(DateTimeOffset.Date));
    }
    else if (property.Type == typeof(DateTime))
    {
      dateExpression = Expression.Property(property, nameof(DateTime.Date));
    }
    else if (property.Type == typeof(DateTime?))
    {
      // For nullable, access .Value.Date
      // This is safe because EF Core handles null checking at SQL level
      var valueProperty = Expression.Property(property, nameof(Nullable<DateTime>.Value));
      dateExpression = Expression.Property(valueProperty, nameof(DateTime.Date));
    }
    else
    {
      // Not a DateTimeOffset field, skip
      action = SyntaxVisitor.Skip;

      return true;
    }

    // Push the date expression onto the instance stack
    // This will be used by nested operation handlers (eq, neq, gt, etc.)
    context.PushInstance(dateExpression);

    // Also push the RuntimeType so the nested handlers know what type to expect
    // The .Date property returns DateTime, so we push that
    if (field.RuntimeType is not null)
    {
      context.RuntimeTypes.Push(field.RuntimeType);
    }

    // Continue processing nested operations
    action = SyntaxVisitor.Continue;

    return true;
  }

  public override bool TryHandleLeave(
    QueryableFilterContext context,
    IFilterField field,
    ObjectFieldNode node,
    [NotNullWhen(true)] out ISyntaxVisitorAction action
  )
  {
    // Pop the instance and runtime type from the stacks
    context.PopInstance();

    if (field.RuntimeType is not null)
    {
      context.RuntimeTypes.Pop();
    }

    action = SyntaxVisitor.Continue;

    return true;
  }
}
