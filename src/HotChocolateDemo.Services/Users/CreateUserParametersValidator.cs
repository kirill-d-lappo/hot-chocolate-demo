using FluentValidation;

namespace HotChocolateDemo.Services.Users;

public class CreateUserParametersValidator : AbstractValidator<CreateUserParameters>
{
  public CreateUserParametersValidator()
  {
    RuleFor(u => u.UserName)
     .NotEmpty()
     .WithErrorCode("Validation.CreateUser.UserName.Required");

    RuleFor(u => u.BirthDateTime)
     .Must(d => (DateTimeOffset.UtcNow - d).Days > 365 * 18)
     .WithErrorCode("Validation.CreateUser.BirthDate.TooYoung")
     .Must(d => d.ToUnixTimeSeconds() > 0)
     .WithErrorCode("Validation.CreateUser.BirthDate.TooOld");
  }
}
