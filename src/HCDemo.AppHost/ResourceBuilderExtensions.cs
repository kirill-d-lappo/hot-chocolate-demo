using System.Diagnostics.CodeAnalysis;

namespace HCDemo.AppHost;

public static class ResourceBuilderExtensions
{
  extension(IResourceBuilder<ParameterResource> builder)
  {
    [return: NotNullIfNotNull(nameof(defaultValue))]
    public string GetNonEmptyValueOrDefault(string defaultValue)
    {
      var value = builder.Resource.Value;
      if (string.IsNullOrWhiteSpace(value))
      {
        return defaultValue;
      }

      return value;
    }

    [return: NotNullIfNotNull(nameof(defaultValue))]
    public async Task<string> GetNonEmptyValueOrDefaultAsync(string defaultValue, CancellationToken ct)
    {
      var value = await builder.Resource.GetValueAsync(ct);
      if (string.IsNullOrWhiteSpace(value))
      {
        return defaultValue;
      }

      return value;
    }
  }
}
