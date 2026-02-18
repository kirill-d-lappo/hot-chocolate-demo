using HotChocolate.Data.Sorting;

namespace HCDemo.Gql.Filters;

public static class QueryableFilteringExtensions
{
  public static IQueryable<T> WhereByFiltering<T>(this IQueryable<T> queryable, IFilterContext filter)
  {
    ArgumentNullException.ThrowIfNull(queryable);

    var predicate = filter?.AsPredicate<T>();

    return predicate is null
      ? queryable
      : queryable.Where(predicate);
  }

  public static IQueryable<T> OrderBySorting<T>(this IQueryable<T> queryable, ISortingContext sorting)
  {
    ArgumentNullException.ThrowIfNull(queryable);

    var sortDefinition = sorting?.AsSortDefinition<T>();

    return sortDefinition is null
      ? queryable
      : queryable.OrderBy(sortDefinition);
  }
}
