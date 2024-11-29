using System.Linq.Expressions;
using HotChocolate.Data.Filters;
using HotChocolate.Pagination;
using HotChocolateDemo.Services.Users;

namespace HotChocolateDemo.Services.Roles;

public interface IRoleProviderService
{
  Task<Role> FindByIdAsync(long id, CancellationToken ct);

  Task<Page<Role>> FindAllByUserIdAsync(long userId, PagingArguments pageArgs, CancellationToken ct);

  Task<Page<Role>> FindAllAsync(
    PagingArguments pageArgs,
    Expression<Func<Role, Role>> selection = default,
    IFilterContext filterContext = default,
    CancellationToken ct = default
  );
}
