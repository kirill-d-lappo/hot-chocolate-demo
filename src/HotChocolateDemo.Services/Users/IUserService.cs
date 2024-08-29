namespace HotChocolateDemo.Services.Users;

public interface IUserService
{
  Task<User> CreateUserAsync(CreateUserParameters parameters, CancellationToken ct);
}