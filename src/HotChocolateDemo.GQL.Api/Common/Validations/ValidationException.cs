using FluentValidation.Results;

namespace HotChocolateDemo.GQL.Api.Common.Validations;

/// <summary>
/// The exception is thrown when a validation is not passed.
/// </summary>
[Serializable]
public class ValidationException : Exception
{
  /// <summary>
  /// Validation exception with specific message
  /// </summary>
  /// <param name="validationResult">Fluent Validation result object.</param>
  /// <param name="message">Error message</param>
  public ValidationException(ValidationResult validationResult, string message)
    : base(message)
  {
    ValidationResult = validationResult;
  }

  /// <summary>
  /// Validation exception with specific message
  /// </summary>
  /// <param name="message">Error message</param>
  public ValidationException(string message)
    : this(null, message)
  {
  }

  public ValidationException()
  {
  }

  /// <summary>
  /// Validation result that caused the exception.
  /// </summary>
  public ValidationResult ValidationResult { get; }
}
