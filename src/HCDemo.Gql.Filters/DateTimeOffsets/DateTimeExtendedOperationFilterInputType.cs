using HCDemo.Gql.Filters.DateTimeOffsets.DateParts;

namespace HCDemo.Gql.Filters.DateTimeOffsets;

/// <summary>
/// Extended DateTimeOffset filter input type that includes a "datePart" field.
/// The datePart field allows filtering by date portion only (ignores time component).
///
/// This filter supports standard DateTimeOffset operations (eq, neq, gt, gte, lt, lte, in, nin)
/// plus a nested "datePart" field for date-only comparisons.
///
/// Usage example in GraphQL:
/// <code>
/// query {
///   allUsers(where: {
///     birthDateTime: {
///       datePart: { eq: "2000-01-15" }
///     }
///   }) {
///     nodes { id userName birthDateTime }
///   }
/// }
/// </code>
///
/// This translates to SQL like:
/// <code>
/// WHERE CONVERT(date, BirthDateTime) = '2000-01-15'
/// </code>
/// </summary>
public class
  DateTimeExtendedOperationFilterInputType : DateTimeOperationFilterInputType //ComparableOperationFilterInputType<DateTimeOffset>
{
  protected override void Configure(IFilterInputTypeDescriptor descriptor)
  {
    base.Configure(descriptor);

    descriptor
      .Field(DateTimeOffsetFilterFields.DatePart)
      .Type<DatePartOperationFilterInputType>()
      .Description(
        "Filters by the date portion only, ignoring time and offset components. "
        + "Values should be in ISO 8601 date format (e.g., \"2000-01-15\")."
      );
  }
}
