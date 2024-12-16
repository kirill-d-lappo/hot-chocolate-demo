using System.Diagnostics.CodeAnalysis;

namespace HotChocolateDemo.GQL;

public static class RequestEndpointBuilderExtensions
{
  public static IEndpointConventionBuilder MapRedirect(
    this IEndpointRouteBuilder builder,
    [StringSyntax("Uri")] string targetPath,
    bool isPermanent = false
  )
  {
    return builder.MapRedirect("/", targetPath, isPermanent);
  }

  public static IEndpointConventionBuilder MapRedirect(
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
