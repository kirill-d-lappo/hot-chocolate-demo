using System.Linq.Expressions;
using GreenDonut.Data;
using HCDemo.Models.UserManagement;

namespace HCDemo.Services.UserManagement.Users;

public interface IUserProviderService
{
  Task<Page<User>> FindAllUsersAsync(
    PagingArguments pageArgs,
    Expression<Func<User, User>> selector = null,
    CancellationToken ct = default
  );
}
