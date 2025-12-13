using FluentValidation;
using HCDemo.Persistence;
using HCDemo.Services.UserManagement.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace HCDemo.Services.UserManagement.Users;

public class UserUpdateService : IUserUpdateService
{
  private readonly IDbContextFactory<HCDemoDbContext> _dbContextFactory;
  private readonly IValidator<UpdateUserParameters> _validator;

  public UserUpdateService(
    IDbContextFactory<HCDemoDbContext> dbContextFactory,
    IValidator<UpdateUserParameters> validator
  )
  {
    _dbContextFactory = dbContextFactory;
    _validator = validator;
  }

  public async Task UpdateUserAsync(UpdateUserParameters parameters, CancellationToken ct)
  {
    await _validator.ValidateAndThrowAsync(parameters, ct);

    var userId = parameters.Id;

    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);
    await using var transaction = await dbContext.Database.BeginTransactionAsync(ct);

    var affectedRows = await dbContext
      .Users
      .Where(u => u.Id == parameters.Id)
      .ExecuteUpdateAsync(
        spc => spc
          .SetProperty(
            o => o.ActivityLevel,
            o => parameters.ActivityLevel.HasValue
              ? parameters.ActivityLevel.Value
              : o.ActivityLevel
          )
          .SetProperty(
            o => o.BirthDateTime,
            o => parameters.BirthDateTime.HasValue
              ? parameters.BirthDateTime.Value
              : o.BirthDateTime
          ),
        ct
      );

    if (affectedRows <= 0)
    {
      throw new UserNotFoundException(userId);
    }

    await transaction.CommitAsync(ct);
  }
}
