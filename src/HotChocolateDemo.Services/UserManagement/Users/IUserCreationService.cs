namespace HotChocolateDemo.Services.UserManagement.Users;

public interface IUserCreationService
{
  Task<long> CreateUserAsync(CreateUserParameters parameters, CancellationToken ct);
}
