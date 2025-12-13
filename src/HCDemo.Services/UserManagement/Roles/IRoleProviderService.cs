using System.Linq.Expressions;
using GreenDonut.Data;
using HotChocolate.Data.Filters;
using HCDemo.Models.UserManagement;

namespace HCDemo.Services.UserManagement.Roles;

public interface IRoleProviderService
{
  Task<Page<Role>> FindAllAsync(
    PagingArguments pageArgs,
    Expression<Func<Role, Role>> selection = null,
    IFilterContext filterContext = null,
    CancellationToken ct = default
  );
}
