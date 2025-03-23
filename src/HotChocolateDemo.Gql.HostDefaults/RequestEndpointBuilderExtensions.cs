using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;

public static class RequestEndpointBuilderExtensions
{
  public static IEndpointConventionBuilder MapGetRedirect(
    this IEndpointRouteBuilder builder,
    [StringSyntax("Uri")] string targetPath,
    bool isPermanent = false
  )
  {
    return builder.MapGetRedirect("/", targetPath, isPermanent);
  }

  public static IEndpointConventionBuilder MapGetRedirect(
    this IEndpointRouteBuilder builder,
    [StringSyntax("Route")] string sourcePattern,
    [StringSyntax("Uri")] string targetPath,
    bool isPermanent = false
  )
  {
    return builder.MapGet(
      sourcePattern,
      context =>
      {
        context.Response.Redirect(targetPath, isPermanent);

        return Task.CompletedTask;
      }
    );
  }
}
