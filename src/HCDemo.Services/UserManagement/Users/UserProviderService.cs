using System.Linq.Expressions;
using GreenDonut.Data;
using HCDemo.Models.UserManagement;
using HCDemo.Persistence;
using HCDemo.Persistence.Models.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace HCDemo.Services.UserManagement.Users;

public class UserProviderService : IUserProviderService
{
  private readonly IDbContextFactory<HCDemoDbContext> _dbContextFactory;

  public UserProviderService(IDbContextFactory<HCDemoDbContext> dbContextFactory)
  {
    _dbContextFactory = dbContextFactory;
  }

  public async Task<Page<User>> FindAllUsersAsync(
    PagingArguments pageArgs,
    Expression<Func<User, User>> selector = null,
    CancellationToken ct = default
  )
  {
    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    return await dbContext
      .Users
      .AsNoTracking()
      .Select(UserSelector)
      .SelectSelection(selector)
      .OrderBy(u => u.Id)
      .ToPageAsync(pageArgs, ct);
  }

  private static Expression<Func<UserEntity, User>> UserSelector { get; } = u => new User
  {
    Id = u.Id,
    UserName = u.UserName,
    BirthDateTime = u.BirthDateTime,
    ActivityLevel = u.ActivityLevel,
    ImageFileName = u.ImageFileName,
  };
}
