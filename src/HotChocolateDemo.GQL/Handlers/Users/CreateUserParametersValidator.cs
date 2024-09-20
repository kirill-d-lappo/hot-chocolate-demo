using FluentValidation;

namespace HotChocolateDemo.GQL.Handlers.Users;

public class UserByUserNameInputValidator : AbstractValidator<UserByUserNameInput>
{
  public UserByUserNameInputValidator()
  {
    RuleFor(u => u.UserName)
     .NotEmpty()
     .WithErrorCode("Validation.CreateUser.UserNameIsRequired");
  }
}
