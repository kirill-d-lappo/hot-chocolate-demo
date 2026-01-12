using System.Linq.Expressions;
using GreenDonut.Data;
using HCDemo.Models.UserManagement;
using HCDemo.Persistence;
using HCDemo.Persistence.Models.UserManagement;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;

namespace HCDemo.Services.UserManagement.Users;

public class UserProviderService : IUserProviderService
{
  private readonly IDbContextFactory<HCDemoDbContext> _dbContextFactory;

  public UserProviderService(IDbContextFactory<HCDemoDbContext> dbContextFactory)
  {
    _dbContextFactory = dbContextFactory;
  }

  public async Task<Page<UserEntity>> FindAllUsersAsync(
    PagingArguments pageArgs,
    IFilterContext filterContext,
    ISortingContext sortingContext,
    ISelection selection,
    CancellationToken ct = default
  )
  {
    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    return await dbContext
      .Users
      .AsNoTracking()
      .OrderBy(u => u.Id)
      // .Select(UserSelector)
      .Select(selection)
      .OrderBySorting(sortingContext)
      .WhereFiltering(filterContext)
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
