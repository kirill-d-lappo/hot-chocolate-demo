﻿using FluentValidation;
using HotChocolateDemo.Models.UserManagement;
using HotChocolateDemo.Services.UserManagement.Users;
using HotChocolateDemo.Services.UserManagement.Users.Errors;

namespace HotChocolateDemo.GQL.Handlers.Users.Mutations.UpdateUsers;

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
}