using FluentValidation;

namespace HCDemo.Services.UserManagement.Users;

public class UpdateUserParametersValidator : AbstractValidator<UpdateUserParameters>
{
  public UpdateUserParametersValidator()
  {
    RuleFor(u => u.Id)
      .GreaterThan(0)
      .WithErrorCode("Validation.UpdateUser.Id.InvalidValue");

    RuleFor(u => u.BirthDateTime.Value)
      .Must(d => (DateTimeOffset.UtcNow - d.Value).Days > 365 * 18)
      .When(d => d.BirthDateTime is { HasValue: true, Value: not null, })
      .WithErrorCode("Validation.UpdateUser.BirthDate.TooYoung")
      .Must(d => d.Value.ToUnixTimeSeconds() > 0)
      .When(d => d.BirthDateTime is { HasValue: true, Value: not null, })
      .WithErrorCode("Validation.UpdateUser.BirthDate.TooOld");

    RuleFor(u => u.ActivityLevel.Value)
      .IsInEnum()
      .When(d => d.ActivityLevel is { HasValue: true, })
      .WithErrorCode("Validation.UpdateUser.ActivityLevel.InvalidValue");
  }
}
