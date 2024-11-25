using FluentValidation;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using HotChocolateDemo.Services.Common.Validations;
using HotChocolateDemo.Services.Users.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotChocolateDemo.Services.Users;

public class OldUserService : IOldUserService
{
  private readonly IValidator<CreateUserParameters> _validator;
  private readonly IDbContextFactory<HCDemoDbContext> _dbContextFactory;
  private readonly ILogger<OldUserService> _logger;

  public OldUserService(
    IValidator<CreateUserParameters> validator,
    IDbContextFactory<HCDemoDbContext> dbContextFactory,
    ILogger<OldUserService> logger
  )
  {
    _validator = validator;
    _dbContextFactory = dbContextFactory;
    _logger = logger;
  }

  public async Task<long> CreateUserAsync(CreateUserParameters parameters, CancellationToken ct)
  {
    await _validator.ThrowWhenNotValidAsync(parameters, ct);

    var userName = parameters.UserName;

    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    var existingUserName = await dbContext
      .Users
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
    };

    await dbContext.Users.AddAsync(user, ct);
    await dbContext.SaveChangesAsync(ct);

    var userId = user.Id;
    _logger.LogInformation("User created: {UserName} {Id}", userName, userId);

    return userId;
  }

  public async Task<User> FindUserByUserNameAsync(string userName, CancellationToken ct)
  {
    if (string.IsNullOrWhiteSpace(userName))
    {
      throw new ArgumentException("Can't be empty", nameof(userName));
    }

    var userEntity = await FindUserEntityByUserNameAsync(userName, ct);

    return new User
    {
      Id = userEntity.Id,
      UserName = userEntity.UserName,
      ActivityLevel = userEntity.ActivityLevel,
    };
  }

  private async Task<UserEntity> FindUserEntityByUserNameAsync(string userName, CancellationToken ct)
  {
    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    return await dbContext
      .Users
      .AsNoTracking()
      .FirstOrDefaultAsync(u => u.UserName == userName, ct);
  }
}
