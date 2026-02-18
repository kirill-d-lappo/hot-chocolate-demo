using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using HCDemo.Gql.Filters;
using HCDemo.Gql.Handlers.Users.Mutations.UpdateUsers;
using HCDemo.Models.UserManagement;
using HCDemo.Persistence;
using HCDemo.Persistence.Models.UserManagement;
using HCDemo.Services.UserManagement.Users;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolate.Execution.Processing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Path = System.IO.Path;

namespace HCDemo.Gql.Handlers.Users.Mutations;

[MutationType]
public class UserExportMutation
{
  private static readonly JsonSerializerOptions JsonOptions = new()
  {
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    WriteIndented = false,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
    Converters =
    {
      new JsonStringEnumConverter(),
    },
  };

  private static readonly CsvConfiguration CsvWriteConfig = new(CultureInfo.InvariantCulture)
  {
    HasHeaderRecord = true,
    TrimOptions = TrimOptions.Trim,
    Mode = CsvMode.RFC4180,
  };

  [UseFiltering<User>]
  [UseSorting<User>]
  public async Task<StartUserExportPayload> StartUserExport(
    ISortingContext sortingContext,
    IFilterContext filterContext,
    HCDemoDbContext dbContext,
    ILogger<UserExportMutation> logger,
    CancellationToken ct
  )
  {
    var options = new ExportOptions
    {
      SortingContext = sortingContext,
      FilterContext = filterContext,
    };

    var optionsJson = JsonSerializer.Serialize(options, JsonOptions);
    var desOptions = JsonSerializer.Deserialize<ExportOptions>(optionsJson, JsonOptions);

    logger.LogInformation("Converted: {OptionsJson}", optionsJson);

    var items = dbContext
      .Users
      .WhereByFiltering(desOptions.FilterContext)
      .OrderBySorting(desOptions.SortingContext)
      .AsAsyncEnumerable();

    var parent = "exported-users";
    if (Directory.Exists(parent))
    {
      Directory.CreateDirectory(parent);
    }

    var filePath = Path.Combine(parent, $"{DateTime.Now:s}.csv");
    await using var fileStream = File.OpenWrite(filePath);
    await using var writer = new StreamWriter(fileStream, Encoding.UTF8);
    await using var csv = new CsvWriter(writer, CsvWriteConfig);

    ConfigureGlobalFormatting(csv);
    csv.Context.RegisterClassMap(new UserEntityClassMap());

    await csv.WriteRecordsAsync(items, ct);

    await csv.FlushAsync();

    return new StartUserExportPayload();
  }

  private static void ConfigureGlobalFormatting(CsvWriter csv)
  {
    var options = new TypeConverterOptions
    {
      // 2009-06-15T13:45:30.0000000-07:00
      Formats = ["yyyy-MM-ddTHH:mm:sszzz",],
    };

    // For writing or reading
    csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);
    csv.Context.TypeConverterOptionsCache.AddOptions<DateTime?>(options);
    csv.Context.TypeConverterOptionsCache.AddOptions<DateTimeOffset>(options);
    csv.Context.TypeConverterOptionsCache.AddOptions<DateTimeOffset?>(options);
  }
}

public sealed class UserEntityClassMap : ClassMap<UserEntity>
{
  public UserEntityClassMap()
  {
    Map(x => x.Id);
    Map(x => x.UserName);
    Map(x => x.ActivityLevel);
  }
}

public class StartUserExportPayload
{
  public bool IsStarted { get; set; } = true;
}

public class ExportOptions
{
  public ISortingContext SortingContext { get; set; }

  public IFilterContext FilterContext { get; set; }
}
