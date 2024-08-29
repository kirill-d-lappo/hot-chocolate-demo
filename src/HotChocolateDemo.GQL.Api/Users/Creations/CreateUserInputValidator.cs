using FluentValidation;

namespace HotChocolateDemo.GQL.Api.Users.Creations;

public class CreateUserInputValidator : AbstractValidator<CreateUserInput>
{
  public CreateUserInputValidator()
  {
    RuleFor(u => u.UserName)
      .NotEmpty()
      .WithErrorCode("USERNAME__EMPTY");

    RuleFor(u => u.BirthDateTime)
      .Must(d => (DateTimeOffset.UtcNow - d).Days > 365 * 18)
      .WithErrorCode("BIRTHDATETIME__TOO_YOUNG")
      .Must(d => d.ToUnixTimeSeconds() > 0)
      .WithErrorCode("BIRTHDATETIME__TOO_OLD");
  }
}
