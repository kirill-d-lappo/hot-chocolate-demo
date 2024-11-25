using FluentValidation;
using HotChocolate.Execution.Processing;
using HotChocolate.Pagination;
using HotChocolate.Types.Pagination;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using HotChocolateDemo.Services.Common.Validations;
using HotChocolateDemo.Services.Users;
using HotChocolateDemo.Services.Users.Errors;
using Microsoft.EntityFrameworkCore;
using ValidationException = HotChocolateDemo.Services.Common.Validations.ValidationException;

namespace HotChocolateDemo.GQL.Handlers.Users;

[QueryType]
public static class UserQuery
{
  [UsePaging]
  [UseProjection]
  [UseFiltering]
  [UseSorting]
  public static Task<Connection<UserEntity>> AllUsers(
    PagingArguments pagingArgs,
    ISelection selection,
    IUserService oldUserService,
    CancellationToken ct
  )
  {
    return oldUserService
      .FindAllUsers(pagingArgs, selection.AsSelector<UserEntity>(), ct)
      .ToConnectionAsync();
  }

  [Error<UserNotFoundException>]
  [Error<ValidationException>]
  public static async Task<UserEntity> UserByUserName(
    [GraphQLNonNullType] UserByUserNameInput input,
    IValidator<UserByUserNameInput> validator,
    HCDemoDbContext dbContext,
    CancellationToken ct
  )
  {
    await validator.ThrowWhenNotValidAsync(input, ct);

    var user = await dbContext
      .Users
      .AsNoTracking()
      .FirstOrDefaultAsync(u => u.UserName == input.UserName, ct);

    if (user == null)
    {
      throw new UserNotFoundException("No user was found by provided user name");
    }

    return user;
  }
}
