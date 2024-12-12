namespace HotChocolateDemo.Services.UserManagement.Users;

public interface IUserImageStorage
{
  Task<string> SaveImageAsync(Stream image, CancellationToken ct);
}
