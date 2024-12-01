using System.Linq.Expressions;
using HotChocolate.Data.Filters;
using HotChocolate.Execution.Processing;

namespace HotChocolateDemo.Persistence;

public static class QueryableExtensions
{
  public static IQueryable<T> WithSelection<T>(this IQueryable<T> queryable, ISelection selection)
  {
    if (selection == null)
    {
      return queryable;
    }

    return queryable.Select(selection);
  }

  public static IQueryable<T> WithSelection<T>(this IQueryable<T> queryable, Expression<Func<T, T>> selector)
  {
    if (selector == null)
    {
      return queryable;
    }

    return queryable.Select(selector);
  }

  public static IQueryable<T> WithFilter<T>(this IQueryable<T> queryable, IFilterContext filterContext)
  {
    if (filterContext == null)
    {
      return queryable;
    }

    return queryable.Where(filterContext);
  }
}
