using HCDemo.Gql.Filters.DateTimeOffsets.DateParts.OperationHandlers;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HCDemo.Gql.Filters.DateTimeOffsets;

public static class DatePartFilteringRegistrations
{
  /// <summary>
  /// Adds the extended DateTimeOffset filtering with datePart support.
  /// This replaces the default DateTimeOffset filter with our extended version
  /// that includes a "datePart" field for date-only comparisons.
  /// </summary>
  public static IRequestExecutorBuilder AddDateTimeExtendedFiltering(this IRequestExecutorBuilder builder)
  {
    return builder.AddConvention<IFilterConvention>(
      new FilterConventionExtension(x => x
        // Bind DateTimeOffset (and nullable) to our extended filter type globally
        .BindRuntimeType<DateTimeOffset, DateTimeExtendedOperationFilterInputType>()
        .BindRuntimeType<DateTimeOffset?, DateTimeExtendedOperationFilterInputType>()
        // Add the custom field handlers for datePart operations
        .AddProviderExtension(
          new QueryableFilterProviderExtension(y => y.AddDatePartFilterHandlers())
        )
      )
    );
  }

  private static IFilterProviderDescriptor<QueryableFilterContext> AddDatePartFilterHandlers(
    this IFilterProviderDescriptor<QueryableFilterContext> context
  )
  {
    return context
      .AddFieldHandler<QueryableDatePartFieldHandler>()

      // DatePart operation handlers - must be registered before default comparable handlers
      .AddFieldHandler<DatePartEqualsHandler>()
      .AddFieldHandler<DatePartNotEqualsHandler>()
      .AddFieldHandler<DatePartGreaterThanHandler>()
      .AddFieldHandler<DatePartGreaterThanOrEqualsHandler>()
      .AddFieldHandler<DatePartLowerThanHandler>()
      .AddFieldHandler<DatePartLowerThanOrEqualsHandler>()
      .AddFieldHandler<DatePartInHandler>()
      .AddFieldHandler<DatePartNotInHandler>();
  }
}
