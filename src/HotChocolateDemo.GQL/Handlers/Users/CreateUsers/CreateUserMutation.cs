using HotChocolateDemo.Services.Common.Validations;
using HotChocolateDemo.Services.UserManagement.Users;
using HotChocolateDemo.Services.UserManagement.Users.Errors;

namespace HotChocolateDemo.GQL.Handlers.Users.CreateUsers;

[MutationType]
public class CreateUserMutation
{
  [Error<UserAlreadyExistsException>]
  [Error<ValidationException>]
  public async Task<CreateUserPayload> CreateUserAsync(
    CreateUserInput input,
    IUserCreationService userCreationService,
    CancellationToken ct
  )
  {
    var createParams = ToCreateUserParams(input);

    var userId = await userCreationService.CreateUserAsync(createParams, ct);

    return ToCreateUserPayload(userId);
  }

  private static CreateUserPayload ToCreateUserPayload(long userId)
  {
    return new CreateUserPayload
    {
      UserId = userId,
    };
  }

  private static CreateUserParameters ToCreateUserParams(CreateUserInput input)
  {
    return new CreateUserParameters
    {
      UserName = input.UserName,
      BirthDateTime = input.BirthDateTime,
    };
  }
}
