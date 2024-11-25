using System.Linq.Expressions;
using HotChocolate.Data.Filters;
using HotChocolate.Execution.Processing;
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
    ISelection selection = default,
    IFilterContext filterContext = default,
    CancellationToken ct = default
  )
  {
    return await _dbContext
      .Users
      .AsNoTracking()
      .OrderBy(u => u.Id)
      .WhereNotNull(filterContext)
      .SelectNotNull(selection)
      .ToPageAsync(pageArgs, ct);
  }
}
