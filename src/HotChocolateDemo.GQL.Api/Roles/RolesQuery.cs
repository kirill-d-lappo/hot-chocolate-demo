using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.GQL.Api.Roles;

[QueryType]
public static class RolesQuery
{
  [UseOffsetPaging]
  [UseProjection]
  [UseFiltering]
  [UseSorting]
  public static IQueryable<RoleEntity> AllRoles(HCDemoDbContext dbContext)
  {
    return dbContext.Roles.AsSplitQuery().AsNoTracking();
  }
}
