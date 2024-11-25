namespace HotChocolateDemo.Services.Users;

public interface IOldUserService
{
  Task<User> FindUserByUserNameAsync(string userName, CancellationToken ct);

  Task<long> CreateUserAsync(CreateUserParameters parameters, CancellationToken ct);
}
