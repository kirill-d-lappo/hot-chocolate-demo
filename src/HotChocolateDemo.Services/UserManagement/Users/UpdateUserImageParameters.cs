namespace HotChocolateDemo.Services.UserManagement.Users;

public class UpdateUserImageParameters
{
  public long UserId { get; set; }

  public Stream ImageStream { get; set; }

  public void Deconstruct(out long userId, out Stream imageStream)
  {
    userId = UserId;
    imageStream = ImageStream;
  }
}
