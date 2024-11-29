using System.Linq.Expressions;
using HotChocolate.Data.Filters;
using HotChocolate.Pagination;

namespace HotChocolateDemo.Services.Users;

public interface IUserProviderService
{
  Task<User> FindUserByIdAsync(long id, CancellationToken ct);

  Task<Page<User>> FindAllUsersAsync(
    PagingArguments pageArgs,
    Expression<Func<User, User>> selection = default,
    IFilterContext filterContext = default,
    CancellationToken ct = default
  );
}
