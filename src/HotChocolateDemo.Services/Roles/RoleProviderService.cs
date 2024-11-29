using System.Linq.Expressions;
using HotChocolate.Data.Filters;
using HotChocolate.Pagination;
using HotChocolateDemo.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Services.Roles;

public class RoleProviderService : IRoleProviderService
{
  private readonly HCDemoDbContext _dbContext;

  public RoleProviderService(HCDemoDbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<Role> FindByIdAsync(long id, CancellationToken ct)
  {
    return await _dbContext
      .Roles
      .AsNoTracking()
      .Select(
        u => new Role
        {
          Id = u.Id,
          Name = u.Name,
        }
      )
      .FirstOrDefaultAsync(u => u.Id == id, ct);
  }

  public async Task<Page<Role>> FindAllAsync(
    PagingArguments pageArgs,
    Expression<Func<Role, Role>> selection = default,
    IFilterContext filterContext = default,
    CancellationToken ct = default
  )
  {
    return await _dbContext
      .Roles
      .AsNoTracking()
      .OrderBy(u => u.Id)
      .WhereNotNull(filterContext)
      .Select(
        u => new Role
        {
          Id = u.Id,
          Name = u.Name,
        }
      )
      .ToPageAsync(pageArgs, ct);
  }

  public async Task<Page<Role>> FindAllByUserIdAsync(
    long userId,
    PagingArguments pageArgs,
    CancellationToken ct = default
  )
  {
    return await _dbContext
      .UserRoles
      .AsNoTracking()
      .Where(r => r.UserId == userId)
      .Select(ur => ur.Role)
      .Select(
        u => new Role
        {
          Id = u.Id,
          Name = u.Name,
        }
      )
      .ToPageAsync(pageArgs, ct);
  }
}
