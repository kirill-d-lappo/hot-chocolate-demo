using GreenDonut.Selectors;
using HotChocolate.Execution.Processing;
using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.GQL.Handlers.Roles;

[QueryType]
public static class RolesQuery
{
  [UsePaging]
  [UseProjection]
  [UseFiltering]
  [UseSorting]
  public static IQueryable<RoleEntity> AllRoles(HCDemoDbContext dbContext)
  {
    return dbContext
      .Roles
      .AsSplitQuery()
      .AsNoTracking();
  }

  public static async Task<RoleEntity> RoleByIdViaDataLoader(
    long roleId,
    ISelection selection,
    IRoleByIdDataLoader dataLoader,
    CancellationToken ct
  )
  {
    return await dataLoader
      .Select(selection)
      .LoadAsync(roleId, ct);
  }
}
