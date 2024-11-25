using System.Linq.Expressions;

namespace HotChocolateDemo.Services;

public static class QueryableExtensions
{
  public static IQueryable<T> SelectProjection<T>(this IQueryable<T> queryable, Expression<Func<T, T>> projection)
  {
    if (projection == null)
    {
      return queryable;
    }

    return queryable.Select(projection);
  }
}
