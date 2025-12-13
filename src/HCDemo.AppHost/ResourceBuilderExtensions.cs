using System.Diagnostics.CodeAnalysis;

namespace HCDemo.AppHost;

public static class ResourceBuilderExtensions
{
  [return: NotNullIfNotNull(nameof(defaultValue))]
  public static string GetNonEmptyValueOrDefault(this IResourceBuilder<ParameterResource> builder, string defaultValue)
  {
    var value = builder.Resource?.Value;
    if (string.IsNullOrWhiteSpace(value))
    {
      return defaultValue;
    }

    return value;
  }
}
