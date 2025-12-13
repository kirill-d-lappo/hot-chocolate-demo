using System.Linq.Expressions;
using HotChocolate.Data.Filters;
using HotChocolate.Execution.Processing;

namespace HCDemo.Persistence;

public static class QueryableSelectionExtensions
{
  public static IQueryable<T> SelectSelection<T>(this IQueryable<T> queryable, ISelection selection)
  {
    if (selection == null)
    {
      return queryable;
    }

    return queryable.Select(selection);
  }

  public static IQueryable<T> SelectSelection<T>(this IQueryable<T> queryable, Expression<Func<T, T>> selector)
  {
    if (selector == null)
    {
      return queryable;
    }

    return queryable.Select(selector);
  }
}
