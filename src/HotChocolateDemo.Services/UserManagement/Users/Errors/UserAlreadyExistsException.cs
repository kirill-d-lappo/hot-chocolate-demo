namespace HotChocolateDemo.Services.UserManagement.Users.Errors;

public class UserAlreadyExistsException : Exception
{
  public UserAlreadyExistsException()
  {
  }

  public UserAlreadyExistsException(string userName)
    : base(FormatMessage(userName))
  {
  }

  public UserAlreadyExistsException(string userName, Exception innerException)
    : base(FormatMessage(userName), innerException)
  {
  }

  private static string FormatMessage(string userName)
  {
    return $"User with the same name was already created: {userName}";
  }
}
