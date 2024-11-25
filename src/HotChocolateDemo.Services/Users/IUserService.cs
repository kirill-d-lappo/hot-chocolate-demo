using System.Linq.Expressions;
using HotChocolate.Pagination;
using HotChocolateDemo.Persistence.Models;

namespace HotChocolateDemo.Services.Business;

public interface IUserService
{
  Task<Page<UserEntity>> FindAllUsers(
    PagingArguments pageArgs,
    Expression<Func<UserEntity, UserEntity>> projection = default,
    CancellationToken ct = default
  );
}
