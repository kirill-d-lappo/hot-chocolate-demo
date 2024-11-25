using System.Linq.Expressions;
using HotChocolate.Pagination;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Services.Users;

public class UserService : IUserService
{
  private readonly HCDemoDbContext _dbContext;

  public UserService(HCDemoDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<Page<UserEntity>> FindAllUsers(
    PagingArguments pageArgs,
    Expression<Func<UserEntity, UserEntity>> projection = default,
    CancellationToken ct = default
  )
  {
    return await _dbContext
      .Users
      .AsNoTracking()
      .OrderBy(u => u.Id)
      .ThenBy(u => u.UserName)
      .SelectProjection(projection)
      .ToPageAsync(pageArgs, ct);
  }
}
