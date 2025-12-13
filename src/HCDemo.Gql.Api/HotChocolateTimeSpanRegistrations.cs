using HotChocolate.Execution.Configuration;

namespace HCDemo.Gql.Api;

// Note [2025-04-07 klappo] TimeSpan will be formatted in Dotnet "hh:mm:ss" format, not ISO PT2H4M
public static class HotChocolateTimeSpanRegistrations
{
  /// <summary>
  /// Setups TimeSpin behaviour in GQL system
  /// </summary>
  /// <param name="builder">GQL builder</param>
  /// <param name="format">String format of TimeSpan value</param>
  /// <param name="bind">Bind behaviour</param>
  /// <returns>Updated GQL builder</returns>
  public static IRequestExecutorBuilder AddTimeSpan(
    this IRequestExecutorBuilder builder,
    TimeSpanFormat format = TimeSpanFormat.DotNet,
    BindingBehavior bind = BindingBehavior.Implicit
  )
  {
    builder
      .BindRuntimeType<TimeSpan, TimeSpanType>()
      .AddType(new TimeSpanType(format, bind));

    return builder;
  }
}
