using HotChocolate.Data.Filters;
using HotChocolate.Execution.Processing;

namespace HotChocolateDemo.Persistence;

public static class QueryableExtensions
{
  public static IQueryable<T> SelectNotNull<T>(this IQueryable<T> queryable, ISelection selection)
  {
    if (selection == null)
    {
      return queryable;
    }

    return queryable.Select(selection);
  }

  public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> queryable, IFilterContext filterContext)
  {
    if (filterContext == null)
    {
      return queryable;
    }

    return queryable.Where(filterContext);
  }
}
