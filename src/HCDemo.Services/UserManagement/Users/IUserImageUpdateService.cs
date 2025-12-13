namespace HCDemo.Services.UserManagement.Users;

public interface IUserImageUpdateService
{
  Task UpdateUserImageAsync(UpdateUserImageParameters parameters, CancellationToken ct);
}
