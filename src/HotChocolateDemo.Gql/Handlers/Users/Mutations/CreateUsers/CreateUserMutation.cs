using FluentValidation;
using HotChocolateDemo.Models.UserManagement;
using HotChocolateDemo.Services.UserManagement.Users;
using HotChocolateDemo.Services.UserManagement.Users.Errors;

namespace HotChocolateDemo.Gql.Handlers.Users.Mutations.CreateUsers;

[MutationType]
public class CreateUserMutation
{
  [Error<UserAlreadyExistsException>]
  [Error<ValidationException>]
  public async Task<User> CreateUserAsync(
    CreateUserInput input,
    IUserCreationService userCreationService,
    IFindUserByIdDataLoader findUserByIdDataLoader,
    CancellationToken ct
  )
  {
    var createParams = ToCreateUserParams(input);

    var userId = await userCreationService.CreateUserAsync(createParams, ct);

    return await findUserByIdDataLoader.LoadAsync(userId, ct);
  }

  private static CreateUserParameters ToCreateUserParams(CreateUserInput input)
  {
    return new CreateUserParameters
    {
      UserName = input.UserName,
      BirthDateTime = input.BirthDateTime,
      ActivityLevel = input.ActivityLevel,
    };
  }
}
