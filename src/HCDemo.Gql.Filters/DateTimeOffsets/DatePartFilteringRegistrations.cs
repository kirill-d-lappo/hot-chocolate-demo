using HCDemo.Gql.Filters.DateTimeOffsets.DateParts.OperationHandlers;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HCDemo.Gql.Filters.DateTimeOffsets;

public static class DatePartFilteringRegistrations
{
  public static IRequestExecutorBuilder AddDateTimeExtendedFiltering(this IRequestExecutorBuilder builder)
  {
    return builder.AddConvention<IFilterConvention>(
      new FilterConventionExtension(x => x
        .OverrideDateTimeOffsetFiltering<DateTimeExtendedOperationFilterInputType>()
        .OverrideDateTimeFiltering<DateTimeExtendedOperationFilterInputType>()

        // Bind DateTimeOffset (and nullable) to our extended filter type globally
        .AddProviderExtension(new QueryableFilterProviderExtension(y => y.AddDatePartFilterHandlers()))
      )
    );
  }

  private static IFilterConventionDescriptor OverrideDateTimeOffsetFiltering<TFilterInputType>(
    this IFilterConventionDescriptor descriptor
  )
    where TFilterInputType : FilterInputType
  {
    return descriptor
      .BindRuntimeType<DateTimeOffset, TFilterInputType>()
      .BindRuntimeType<DateTimeOffset?, TFilterInputType>();
  }

  private static IFilterConventionDescriptor OverrideDateTimeFiltering<TFilterInputType>(
    this IFilterConventionDescriptor descriptor
  )
    where TFilterInputType : FilterInputType
  {
    return descriptor
      .BindRuntimeType<DateTime, TFilterInputType>()
      .BindRuntimeType<DateTime?, TFilterInputType>();
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
