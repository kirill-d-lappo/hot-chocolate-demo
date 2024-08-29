using FluentValidation;
using HotChocolateDemo.GQL.Api.Common.Validations;
using HotChocolateDemo.GQL.Api.Users.Creations;
using HotChocolateDemo.GQL.Api.Users.Errors;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using ValidationException = HotChocolateDemo.GQL.Api.Common.Validations.ValidationException;

namespace HotChocolateDemo.GQL.Api.Users;

[MutationType]
public class UserMutation
{
  [Error<UserAlreadyExistsException>]
  [Error<ValidationException>]
  public async Task<UserEntity> CreateUserAsync(
    CreateUserInput input,
    IValidator<CreateUserInput> validator,
    HCDemoDbContext dbContext,
    CancellationToken ct)
  {
    await validator.ThrowWhenNotValidAsync(input, ct);

    var userName = input.UserName;

    var existingUserName = await dbContext.Users
      .AsNoTracking()
      .Select(u => u.UserName)
      .FirstOrDefaultAsync(u => u == userName, ct);

    if (existingUserName != default)
    {
      throw new UserAlreadyExistsException();
    }

    var user = new UserEntity
    {
      UserName = input.UserName,
    };

    await dbContext.Users.AddAsync(user, ct);
    await dbContext.SaveChangesAsync(ct);

    var userId = user.Id;

    return await dbContext.Users
      .Where(u => u.Id == userId)
      .FirstOrDefaultAsync(ct);
  }
}
