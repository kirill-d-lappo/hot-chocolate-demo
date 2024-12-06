namespace HotChocolateDemo.Services.UserManagement.Users;

public interface IUserUpdateService
{
  Task UpdateUserAsync(UpdateUserParameters parameters, CancellationToken ct);
}
