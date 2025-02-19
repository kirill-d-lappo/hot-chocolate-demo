using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using HotChocolate.Execution.Processing;
using HotChocolate.Resolvers;

namespace HotChocolateDemo.Gql.Diagnostics;

// ReSharper disable once ClassNeverInstantiated.Global
internal class ErrorLoggingDiagnosticsEventListener : ExecutionDiagnosticEventListener
{
  private readonly ILogger<ErrorLoggingDiagnosticsEventListener> _logger;

  public ErrorLoggingDiagnosticsEventListener(ILogger<ErrorLoggingDiagnosticsEventListener> logger)
  {
    _logger = logger;
  }

  public override void ResolverError(IMiddlewareContext context, IError error)
  {
    _logger.LogError(error.Exception, "Resolver Error - {ErrorMessage}", error.Message);
  }

  public override void TaskError(IExecutionTask task, IError error)
  {
    _logger.LogError(error.Exception, "Task Error - {ErrorMessage}", error.Message);
  }

  public override void RequestError(IRequestContext context, Exception exception)
  {
    _logger.LogError(exception, "RequestError - {ErrorMessage}", exception.Message);
  }

  public override void SubscriptionEventError(SubscriptionEventContext context, Exception exception)
  {
    _logger.LogError(exception, "SubscriptionEventError - {ErrorMessage}", exception.Message);
  }

  public override void SubscriptionTransportError(ISubscription subscription, Exception exception)
  {
    _logger.LogError(exception, "SubscriptionTransportError - {ErrorMessage}", exception.Message);
  }
}
