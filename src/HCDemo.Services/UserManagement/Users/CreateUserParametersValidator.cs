using FluentValidation;

namespace HCDemo.Services.UserManagement.Users;

public class CreateUserParametersValidator : AbstractValidator<CreateUserParameters>
{
  public CreateUserParametersValidator()
  {
    RuleFor(u => u.UserName)
      .NotEmpty()
      .WithErrorCode("Validation.CreateUser.UserName.Required");

    RuleFor(u => u.BirthDateTime)
      .Must(d => (DateTimeOffset.UtcNow - d.Value).Days > 365 * 18)
      .When(d => d.BirthDateTime.HasValue)
      .WithErrorCode("Validation.CreateUser.BirthDate.TooYoung")
      .Must(d => d.Value.ToUnixTimeSeconds() > 0)
      .When(d => d.BirthDateTime.HasValue)
      .WithErrorCode("Validation.CreateUser.BirthDate.TooOld");
  }
}
