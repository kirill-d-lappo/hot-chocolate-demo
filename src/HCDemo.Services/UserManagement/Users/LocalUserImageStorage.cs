using Microsoft.Extensions.Logging;

namespace HCDemo.Services.UserManagement.Users;

public class LocalUserImageStorage : IUserImageStorage
{
  private readonly ILogger<LocalUserImageStorage> _logger;

  public LocalUserImageStorage(ILogger<LocalUserImageStorage> logger)
  {
    _logger = logger;
  }

  private static DirectoryInfo RootPath
  {
    get
    {
      if (field != null)
      {
        return field;
      }

      field = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "images"));
      if (!field.Exists)
      {
        field.Create();
      }

      return field;
    }
  }

  public async Task<string> SaveImageAsync(Stream image, CancellationToken ct)
  {
    var rootPath = RootPath;
    var fileId = $"{Guid.NewGuid():N}.jpg";
    var filePath = Path.Combine(rootPath.FullName, fileId);

    await using var fs = File.OpenWrite(filePath);

    await image.CopyToAsync(fs, ct);
    await fs.FlushAsync(ct);

    _logger.LogInformation("Created file: {FilePath}", filePath);

    return fileId;
  }
}
