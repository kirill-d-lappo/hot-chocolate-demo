// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.Hosting;

public static class HostStartExtensions
{
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
