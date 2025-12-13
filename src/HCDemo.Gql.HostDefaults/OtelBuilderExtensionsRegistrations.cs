using System.Reflection;
using OpenTelemetry.Resources;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;

public static class OtelBuilderExtensionsRegistrations
{
  public static ResourceBuilder AddVersion(this ResourceBuilder builder, string version = null)
  {
    version ??= Assembly
      .GetExecutingAssembly()
      .GetName()
      .Version
      ?.ToString();

    if (string.IsNullOrWhiteSpace(version))
    {
      return builder;
    }

    return builder.AddAttribute("service.version", version);
  }

  public static ResourceBuilder AddEnvironment(this ResourceBuilder builder, string environmentName)
  {
    return builder.AddAttribute("service.environment", environmentName);
  }

  public static ResourceBuilder AddAttribute(this ResourceBuilder builder, string key, string value)

  {
    return builder.AddAttributes([new KeyValuePair<string, object>(key, value),]);
  }
}
