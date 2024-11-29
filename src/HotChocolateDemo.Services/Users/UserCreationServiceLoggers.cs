using HotChocolateDemo.Persistence.Models;
using Microsoft.Extensions.Logging;

namespace HotChocolateDemo.Services.Users;

internal static partial class UserCreationServiceLoggers
{
  [LoggerMessage(Level = LogLevel.Information, Message = "User was created: {@User}")]
  public static partial void UserWasCreated(this ILogger logger, UserEntity user);
}
