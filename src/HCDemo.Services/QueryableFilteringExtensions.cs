using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;

namespace HCDemo.Services;

public static class QueryableFilteringExtensions
{
  public static IQueryable<T> WhereFiltering<T>(this IQueryable<T> queryable, IFilterContext filter)
  {
    if (queryable is null)
    {
      throw new ArgumentNullException(nameof(queryable));
    }

    var predicate = filter?.AsPredicate<T>();

    return predicate is null
      ? queryable
      : queryable.Where(predicate);
  }

  public static IQueryable<T> OrderBySorting<T>(this IQueryable<T> queryable, ISortingContext sorting)
  {
    if (queryable is null)
    {
      throw new ArgumentNullException(nameof(queryable));
    }

    var predicate = sorting?.AsSortDefinition<T>();

    return predicate is null
      ? queryable
      : queryable.OrderBy(predicate);
  }
}
