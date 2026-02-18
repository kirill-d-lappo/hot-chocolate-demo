using System.Linq.Expressions;
using HotChocolate.Execution.Processing;

namespace HCDemo.Gql.Filters;

public static class QueryableSelectionExtensions
{
  public static IQueryable<T> SelectBySelection<T>(this IQueryable<T> queryable, ISelection selection)
  {
    ArgumentNullException.ThrowIfNull(queryable);

    return selection == null
      ? queryable
      : queryable.Select(selection);
  }

  public static IQueryable<T> SelectBySelection<T>(this IQueryable<T> queryable, Expression<Func<T, T>> selector)
  {
    ArgumentNullException.ThrowIfNull(queryable);

    return selector == null
      ? queryable
      : queryable.Select(selector);
  }
}
