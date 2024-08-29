using System.Diagnostics;
using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.Results;

namespace HotChocolateDemo.Services.Common.Validations;

/// <summary>
/// Helper methods to run on validation rule
/// </summary>
public static class ValidatorExtensions
{
  /// <summary>
  /// Validate the specified instance asynchronously
  /// </summary>
  /// <param name="validator">Validator that is used to validate object.</param>
  /// <param name="instance">The instance to validate</param>
  /// <param name="ct">Cancellation token.</param>
  /// <param name="callerName">The name of code block that runs validation.</param>
  /// <typeparam name="T">Type of object instance.</typeparam>
  /// <exception cref="FluentValidation.ValidationException">When validation is not passed.</exception>
  /// <returns>A <see cref="ValidationResult"/> object containing any validation failures.</returns>
  [StackTraceHidden]
  [DebuggerStepThrough]
  public static async Task<ValidationResult> ThrowWhenNotValidAsync<T>(
    this IValidator<T> validator,
    T instance,
    CancellationToken ct,
    [CallerMemberName] string callerName = default
  )
  {
    var validationResult = await validator.ValidateAsync(instance, ct);
    if (!validationResult.IsValid)
    {
      var message = $"Validation error at {callerName ?? "unknown method"}: {validationResult}";

      throw new ValidationException(validationResult, message);
    }

    return validationResult;
  }
}
