using FluentValidation;

namespace HotChocolateDemo.GQL.Handlers.Users.CreateUsers;

public class CreateUserInputValidator : AbstractValidator<CreateUserInput>
{
  public CreateUserInputValidator()
  {
    RuleFor(u => u.UserName)
      .NotEmpty()
      .WithErrorCode("Validation.CreateUser.UserNameIsRequired");
  }
}
