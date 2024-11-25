using System.Linq.Expressions;
using HotChocolate.Data.Filters;
using HotChocolate.Execution.Processing;
using HotChocolate.Pagination;
using HotChocolateDemo.Persistence.Models;

namespace HotChocolateDemo.Services.Users;

public interface IUserService
{
  Task<Page<UserEntity>> FindAllUsers(
    PagingArguments pageArgs,
    ISelection selection = default,
    IFilterContext filterContext = default,
    CancellationToken ct = default
  );
}
