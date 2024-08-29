using FluentValidation;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using HotChocolateDemo.Services.Common.Validations;
using HotChocolateDemo.Services.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Services.Users;

public class UserService : IUserService
{
  private readonly IValidator<CreateUserParameters> _validator;
  private readonly IDbContextFactory<HCDemoDbContext> _dbContextFactory;

  public UserService(IValidator<CreateUserParameters> validator, IDbContextFactory<HCDemoDbContext> dbContextFactory)
  {
    _validator = validator;
    _dbContextFactory = dbContextFactory;
  }

  public async Task<User> CreateUserAsync(CreateUserParameters parameters, CancellationToken ct)
  {
    await _validator.ThrowWhenNotValidAsync(parameters, ct);

    var userName = parameters.UserName;

    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    var existingUserName = await dbContext.Users
      .AsNoTracking()
      .Select(u => u.UserName)
      .FirstOrDefaultAsync(u => u == userName, ct);

    if (existingUserName != default)
    {
      throw new UserAlreadyExistsException(existingUserName);
    }

    var user = new UserEntity
    {
      UserName = userName,
      // FixMe [2024-01-01 klappo] add birthday
    };

    await dbContext.Users.AddAsync(user, ct);
    await dbContext.SaveChangesAsync(ct);

    var userId = user.Id;

    user = await dbContext.Users
      .Where(u => u.Id == userId)
      .FirstOrDefaultAsync(ct);

    return new User
    {
      Id = user.Id,
      UserName = user.UserName,

      // FixMe [2024-01-01 klappo] add birthday to entity
      BirthDateTime = parameters.BirthDateTime,
    };
  }
}
