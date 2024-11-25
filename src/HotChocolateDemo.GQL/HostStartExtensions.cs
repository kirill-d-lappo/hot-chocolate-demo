namespace HotChocolateDemo.GQL;

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

  public static async Task<TResult> RunWithConsoleCancellationAsync<THost, TResult>(
    this THost host,
    Func<THost, CancellationToken, Task<TResult>> action
  )
    where THost : class, IHost
  {
    if (host == null)
    {
      throw new ArgumentNullException(nameof(host));
    }

    if (action == null)
    {
      throw new ArgumentNullException(nameof(action));
    }

    using var cts = new CancellationTokenSource();
    var cancellationHandler = GetCancellationHandler(cts);
    Console.CancelKeyPress += cancellationHandler;

    try
    {
      return await action(host, cts.Token)
        .ConfigureAwait(false);
    }
    finally
    {
      Console.CancelKeyPress -= cancellationHandler;
    }
  }

  private static ConsoleCancelEventHandler GetCancellationHandler(CancellationTokenSource cts)
  {
    return (s, e) =>
    {
      if (!cts.IsCancellationRequested)
      {
        cts.Cancel();
      }

      e.Cancel = true;
    };
  }
}
