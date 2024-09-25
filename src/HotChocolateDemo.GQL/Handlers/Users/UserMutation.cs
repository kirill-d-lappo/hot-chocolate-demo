using HotChocolateDemo.Persistence;
using HotChocolateDemo.Services.Common.Validations;
using HotChocolateDemo.Services.Users;
using HotChocolateDemo.Services.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.GQL.Handlers.Users;

[MutationType]
public class UserMutation
{
  [Error<UserAlreadyExistsException>]
  [Error<ValidationException>]
  public async Task<User> CreateUserAsync(
    CreateUserInput input,
    IUserService userService,
    IDbContextFactory<HCDemoDbContext> dbContextFactory,
    CancellationToken ct
  )
  {
    var createParams = new CreateUserParameters
    {
      UserName = input.UserName,
      BirthDateTime = input.BirthDateTime,
    };

    var user = await userService.CreateUserAsync(createParams, ct);

    var dbContext = await dbContextFactory.CreateDbContextAsync(ct);

    var userEntity = dbContext.Users.FirstOrDefault(u => u.UserName == input.UserName);

    user.Id = userEntity?.Id ?? 0;

    return user;
  }
}
