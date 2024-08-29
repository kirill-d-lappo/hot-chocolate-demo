using HotChocolateDemo.Persistence;
using HotChocolateDemo.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace HotChocolateDemo.GQL.Api.UserRoles;

[QueryType]
public static class UserRoleQuery
{
  [UsePaging]
  [UseProjection]
  [UseFiltering]
  [UseSorting]
  public static IQueryable<UserRoleEntity> AllUserRoles(HCDemoDbContext dbContext)
  {
    return dbContext.UserRoles
      .AsSplitQuery()
      .AsNoTracking();
  }
}
