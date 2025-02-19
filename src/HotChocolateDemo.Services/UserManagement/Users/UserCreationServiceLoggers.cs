using HotChocolateDemo.Persistence.Models.UserManagement;
using Microsoft.Extensions.Logging;

namespace HotChocolateDemo.Services.UserManagement.Users;

internal static partial class UserCreationServiceLoggers
{
  [LoggerMessage(Level = LogLevel.Information, Message = "User was created: {@User}")]
  public static partial void UserWasCreated(this ILogger logger, UserEntity user);
}
