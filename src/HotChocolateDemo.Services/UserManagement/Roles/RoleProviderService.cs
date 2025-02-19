using System.Linq.Expressions;
using GreenDonut.Data;
using HotChocolate.Data.Filters;
using HotChocolateDemo.Models.UserManagement;
using HotChocolateDemo.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.Services.UserManagement.Roles;

public class RoleProviderService : IRoleProviderService
{
  private readonly IDbContextFactory<HCDemoDbContext> _dbContextFactory;

  public RoleProviderService(IDbContextFactory<HCDemoDbContext> dbContextFactory)
  {
    _dbContextFactory = dbContextFactory;
  }

  public async Task<Page<Role>> FindAllAsync(
    PagingArguments pageArgs,
    Expression<Func<Role, Role>> selection = null,
    IFilterContext filterContext = null,
    CancellationToken ct = default
  )
  {
    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    return await dbContext
      .Roles
      .AsNoTracking()
      .WithFilter(filterContext)
      .WithSelection(selection)
      .OrderBy(u => u.Id)
      .ToPageAsync(pageArgs, ct);
  }
}
