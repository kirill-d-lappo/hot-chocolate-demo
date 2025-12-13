using HCDemo.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HCDemo.Services.UserManagement.Users;

internal class UserImageUpdateService : IUserImageUpdateService
{
  private readonly IDbContextFactory<HCDemoDbContext> _dbContextFactory;
  private readonly ILogger<UserImageUpdateService> _logger;
  private readonly IUserImageStorage _imageStorage;

  public UserImageUpdateService(
    IDbContextFactory<HCDemoDbContext> dbContextFactory,
    ILogger<UserImageUpdateService> logger,
    IUserImageStorage imageStorage
  )
  {
    _dbContextFactory = dbContextFactory;
    _logger = logger;
    _imageStorage = imageStorage;
  }

  public async Task UpdateUserImageAsync(UpdateUserImageParameters parameters, CancellationToken ct)
  {
    var (userId, imageStream) = parameters;

    var fileId = await _imageStorage.SaveImageAsync(imageStream, ct);

    await UpdateUserImageNameAsync(userId, fileId, ct);
  }

  private async Task UpdateUserImageNameAsync(long userId, string imageFileName, CancellationToken ct)
  {
    await using var dbContext = await _dbContextFactory.CreateDbContextAsync(ct);

    await dbContext
      .Users
      .Where(u => u.Id == userId)
      .ExecuteUpdateAsync(scp => scp.SetProperty(u => u.ImageFileName, imageFileName), ct);
  }
}
