using FluentValidation;
using HCDemo.Models.UserManagement;
using HCDemo.Services.UserManagement.Users;
using HCDemo.Services.UserManagement.Users.Errors;

namespace HCDemo.Gql.Handlers.Users.Mutations.UpdateUsers;

[MutationType]
public class UpdateUserMutation
{
  [Error<UserNotFoundException>]
  [Error<ValidationException>]
  public async Task<User> UpdateUserAsync(
    UpdateUserInput input,
    IUserUpdateService userCreationService,
    IFindUserByIdDataLoader findUserByIdDataLoader,
    CancellationToken ct
  )
  {
    var userId = input.Id;
    var updateParams = ToUpdateUserParams(input);

    await userCreationService.UpdateUserAsync(updateParams, ct);

    return await findUserByIdDataLoader.LoadAsync(userId, ct);
  }

  private static UpdateUserParameters ToUpdateUserParams(UpdateUserInput input)
  {
    return new UpdateUserParameters
    {
      Id = input.Id,
      BirthDateTime = input.BirthDateTime,
      ActivityLevel = input.ActivityLevel,
    };
  }

  [Error<UserNotFoundException>]
  public async Task<User> UpdateUserImage(
    [GraphQLNonNullType] UpdateUserImageInput input,
    IUserImageUpdateService updateService,
    IFindUserByIdDataLoader findUserByIdDataLoader,
    CancellationToken ct
  )
  {
    var userId = input.UserId;
    var imageFile = input.File;
    var updateParameters = new UpdateUserImageParameters
    {
      UserId = userId,
      ImageStream = imageFile.OpenReadStream(),
    };

    await updateService.UpdateUserImageAsync(updateParameters, ct);

    return await findUserByIdDataLoader.LoadAsync(userId, ct);
  }
}
