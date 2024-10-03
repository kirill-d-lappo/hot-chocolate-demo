using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using HotChocolateDemo.Services.Common.Validations;
using HotChocolateDemo.Services.Users;
using HotChocolateDemo.Services.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.GQL.Handlers.Users;

[MutationType]
public class UserMutation
{
  [UseFirstOrDefault]
  [UseProjection]
  [Error<UserAlreadyExistsException>]
  [Error<ValidationException>]
  public async Task<IQueryable<UserEntity>> CreateUserAsync(
    CreateUserInput input,
    IUserService userService,
    HCDemoDbContext dbContext,
    CancellationToken ct
  )
  {
    var createParams = new CreateUserParameters
    {
      UserName = input.UserName,
      BirthDateTime = input.BirthDateTime,
    };

    var userId = await userService.CreateUserAsync(createParams, ct);

    return dbContext
     .Users
     .AsNoTracking()
     .Where(u => u.Id == userId);
  }
}
