namespace HotChocolateDemo.Services.Users;

public interface IUserCreationService
{
  Task<long> CreateUserAsync(CreateUserParameters parameters, CancellationToken ct);
}
