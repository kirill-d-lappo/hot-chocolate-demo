using HotChocolate.Execution.Configuration;
using HCDemo.Gql.Api.Diagnostics;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Error Logging Extension Methods for <see cref="IRequestExecutorBuilder"/>
/// </summary>
public static class ErrorLoggingExtensions
{
  /// <summary>
  /// Adds event logger which logs all errors from HotChocolate GraphQL engine.
  /// </summary>
  /// <param name="builder">GQL Service Builder to update</param>
  /// <returns>Updated GQL Service Builder</returns>
  public static IRequestExecutorBuilder AddErrorLogging(this IRequestExecutorBuilder builder)
  {
    builder.AddDiagnosticEventListener<ErrorLoggingDiagnosticsEventListener>();

    return builder;
  }
}
