using System.Linq.Expressions;
using GreenDonut.Data;
using HCDemo.Models.UserManagement;
using HCDemo.Persistence.Models.UserManagement;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolate.Execution.Processing;

namespace HCDemo.Services.UserManagement.Users;

public interface IUserProviderService
{
  Task<Page<UserEntity>> FindAllUsersAsync(
    PagingArguments pageArgs,
    IFilterContext filterContext,
    ISortingContext sortingContext,
    ISelection selection,
    CancellationToken ct
  );
}
