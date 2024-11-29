using System.Linq.Expressions;
using HotChocolate.Data.Filters;
using HotChocolate.Pagination;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Services.Users;

public class UserProviderService : IUserProviderService
{
  private readonly HCDemoDbContext _dbContext;

  public UserProviderService(HCDemoDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<User> FindUserByIdAsync(long id, CancellationToken ct)
  {
    return await _dbContext
      .Users
      .AsNoTracking()
      .Select(
        u => new User
        {
          Id = u.Id,
          UserName = u.UserName,
          ActivityLevel = u.ActivityLevel,
          BirthDateTime = u.BirthDate,
        }
      )
      .FirstOrDefaultAsync(u => u.Id == id, ct);
  }

  public async Task<Page<User>> FindAllUsersAsync(
    PagingArguments pageArgs,
    Expression<Func<User, User>> selection = default,
    IFilterContext filterContext = default,
    CancellationToken ct = default
  )
  {
    return await _dbContext
      .Users
      .AsNoTracking()
      .OrderBy(u => u.Id)
      .WhereNotNull(filterContext)
      .Select(
        u => new User
        {
          Id = u.Id,
          UserName = u.UserName,
          ActivityLevel = u.ActivityLevel,
          BirthDateTime = u.BirthDate,
        }
      )

      // .SelectNotNull(selection)
      .ToPageAsync(pageArgs, ct);
  }
}
