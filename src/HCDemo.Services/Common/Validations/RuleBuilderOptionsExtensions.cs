using FluentValidation;

namespace HCDemo.Services.Common.Validations;

/// <summary>
/// Helper extensions to builder options
/// </summary>
public static class RuleBuilderOptionsExtensions
{
  /// <summary>
  /// Configures rule to be applied only when target property is not null or whitespace.
  /// </summary>
  /// <param name="ruleBuilderOptions">Rule builder options to configure.</param>
  /// <param name="applyConditionTo">Whether the condition should be applied to the current property validator in the chain, or all preceding property validators in the chain.</param>
  /// <typeparam name="T">Type of target object</typeparam>
  /// <returns>Enriched rule builder options.</returns>
  public static IRuleBuilderOptions<T, string> WhenNotNullOrWhitespace<T>(
    this IRuleBuilderOptions<T, string> ruleBuilderOptions,
    ApplyConditionTo applyConditionTo = ApplyConditionTo.AllValidators
  )
  {
    return ruleBuilderOptions.Configure(r =>
      {
        r.ApplyCondition(
          c => !string.IsNullOrWhiteSpace(
            r
              .GetPropertyValue(c.InstanceToValidate)
              ?.ToString()
          ),
          applyConditionTo
        );
      }
    );
  }
}
