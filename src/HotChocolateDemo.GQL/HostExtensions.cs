namespace HotChocolateDemo.GQL;

public static class HostExtensions
{
  public static async Task<int> RunWithCliAsync<THost>(
    this THost app,
    string[] args,
    Func<THost, string[], CancellationToken, Task> appAction,
    CancellationToken ct
  )
    where THost : class, IHost
  {
    if (IsGraphQLCommand(args))
    {
      return await app.RunWithGraphQLCommandsAsync(args);
    }

    await appAction(app, args, ct);

    return 0;

    static bool IsGraphQLCommand(string[] args)
    {
      return args is ["schema", ..,];
    }
  }
}
