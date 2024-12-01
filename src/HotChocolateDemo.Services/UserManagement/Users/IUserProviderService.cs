using System.Linq.Expressions;
using HotChocolate.Pagination;
using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.Services.UserManagement.Users;

public interface IUserProviderService
{
  Task<Page<User>> FindAllUsersAsync(
    PagingArguments pageArgs,
    Expression<Func<User, User>> selector = default,
    CancellationToken ct = default
  );
}
