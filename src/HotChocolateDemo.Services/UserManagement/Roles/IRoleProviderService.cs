using System.Linq.Expressions;
using HotChocolate.Data.Filters;
using HotChocolate.Pagination;
using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.Services.UserManagement.Roles;

public interface IRoleProviderService
{
  Task<Page<Role>> FindAllAsync(
    PagingArguments pageArgs,
    Expression<Func<Role, Role>> selection = default,
    IFilterContext filterContext = default,
    CancellationToken ct = default
  );
}
