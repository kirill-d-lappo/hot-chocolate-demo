namespace HotChocolateDemo.Gql;

public static class HostStartExtensions
{
  public static async Task<int> RunWithGqlCliAsync<THost>(
    this THost app,
    string[] args,
    Func<THost, string[], CancellationToken, Task<int>> appAction,
    CancellationToken ct = default
  )
    where THost : class, IHost
  {
    if (IsGraphQLCommand(args))
    {
      return await app.RunWithGraphQLCommandsAsync(args);
    }

    return await appAction(app, args, ct);
  }

  private static bool IsGraphQLCommand(string[] args)
  {
    return args is ["schema", ..,];
  }
}
