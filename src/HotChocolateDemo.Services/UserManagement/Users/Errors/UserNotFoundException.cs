namespace HotChocolateDemo.Services.UserManagement.Users.Errors;

public class UserNotFoundException : Exception
{
  public UserNotFoundException()
  {
  }

  public UserNotFoundException(long userId, Exception innerException = null)
    : base($"User {userId} was not found", innerException)
  {
    UserId = userId;
  }

  public long UserId { get; set; }
}
