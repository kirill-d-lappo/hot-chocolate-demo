using FluentValidation;
using HotChocolateDemo.Models.UserManagement;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Services.UserManagement.Users.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotChocolateDemo.Services.UserManagement.Users;

public class UserCreationService : IUserCreationService
{
  private readonly IDbContextFactory<HCDemoDbContext> _dbContextFactory;
  private readonly IValidator<CreateUserParameters> _validator;
  private readonly ILogger<UserCreationService> _logger;

  public UserCreationService(
    IDbContextFactory<HCDemoDbContext> dbContextFactory,
    IValidator<CreateUserParameters> validator,
    ILogger<UserCreationService> logger
  )
  {
    _dbContextFactory = dbContextFactory;
    _validator = validator;
    _logger = logger;
  }

  public async Task<long> CreateUserAsync(CreateUserParameters parameters, CancellationToken ct)
  {
    await _validator.ValidateAndThrowAsync(parameters, ct);

    var userName = parameters.UserName;

    var user = new User
    {
      UserName = userName,
      BirthDateTime = parameters.BirthDateTime,
      ActivityLevel = parameters.ActivityLevel,
    };

    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);
    await dbContext.Users.AddAsync(user, ct);

    try
    {
      await dbContext.SaveChangesAsync(ct);
    }
    catch (DbUpdateException e)
    {
      throw new UserAlreadyExistsException(userName, e);
    }

    var userId = user.Id;

    _logger.UserWasCreated(user);

    return userId;
  }
}
