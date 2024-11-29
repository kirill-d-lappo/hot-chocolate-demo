using FluentValidation;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using HotChocolateDemo.Services.Common.Validations;
using HotChocolateDemo.Services.Users.Errors;
using Microsoft.Extensions.Logging;

namespace HotChocolateDemo.Services.Users;

public class UserCreationService : IUserCreationService
{
  private readonly HCDemoDbContext _dbContext;
  private readonly IValidator<CreateUserParameters> _validator;
  private readonly ILogger<UserCreationService> _logger;

  public UserCreationService(
    HCDemoDbContext dbContext,
    IValidator<CreateUserParameters> validator,
    ILogger<UserCreationService> logger
  )
  {
    _dbContext = dbContext;
    _validator = validator;
    _logger = logger;
  }

  public async Task<long> CreateUserAsync(CreateUserParameters parameters, CancellationToken ct)
  {
    await _validator.ThrowWhenNotValidAsync(parameters, ct);

    var userName = parameters.UserName;

    var user = new UserEntity
    {
      UserName = userName,
    };

    await _dbContext.Users.AddAsync(user, ct);

    try
    {
      await _dbContext.SaveChangesAsync(ct);
    }

    // FixMe [2024-11-29 klappo] set specific exception type here
    catch (Exception e)
    {
      throw new UserAlreadyExistsException(userName, e);
    }

    var userId = user.Id;

    _logger.UserWasCreated(user);

    return userId;
  }
}
