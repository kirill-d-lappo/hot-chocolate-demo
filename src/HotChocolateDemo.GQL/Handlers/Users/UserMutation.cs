using HotChocolateDemo.Services.Common.Validations;
using HotChocolateDemo.Services.Users;
using HotChocolateDemo.Services.Users.Errors;

namespace HotChocolateDemo.GQL.Handlers.Users;

[MutationType]
public class UserMutation
{
  [Error<UserAlreadyExistsException>]
  [Error<ValidationException>]
  public async Task<User> CreateUserAsync(CreateUserInput input, IUserService userService, CancellationToken ct)
  {
    var createParams = new CreateUserParameters
    {
      UserName = input.UserName,
      BirthDateTime = input.BirthDateTime,
    };

    var user = await userService.CreateUserAsync(createParams, ct);

    return user;
  }
}
