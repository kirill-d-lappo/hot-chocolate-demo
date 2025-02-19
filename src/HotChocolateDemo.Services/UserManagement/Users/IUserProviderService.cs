using System.Linq.Expressions;
using GreenDonut.Data;
using HotChocolateDemo.Models.UserManagement;

namespace HotChocolateDemo.Services.UserManagement.Users;

public interface IUserProviderService
{
  Task<Page<User>> FindAllUsersAsync(
    PagingArguments pageArgs,
    Expression<Func<User, User>> selector = null,
    CancellationToken ct = default
  );
}
