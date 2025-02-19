using HotChocolate.Diagnostics;
using HotChocolateDemo.Gql.Filters;
using HotChocolateDemo.Persistence;

namespace HotChocolateDemo.Gql;

public static class GqlRegistrations
{
  public static IServiceCollection AddGqlServices(this IServiceCollection services)
  {
    // Note [2025-01-23 klappo] that hash provider is used in persisted operations
    // FixMe [2025-01-23 klappo] figure it out how it is used in HC pipeline, atm the pipeline just grabs file by its name
    services.AddMD5DocumentHashProvider();

    services

      // Note [2024-11-25 klappo] costs calculation is disabled for a while
      .AddGraphQLServer(disableDefaultSecurity: true)
      .InitializeOnStartup()
      .AddErrorLogging()
      .ModifyOptions(
        o =>
        {
          o.SortFieldsByName = true;
        }
      )
      .AddQueryType()
      .AddQueryConventions()
      .AddMutationType()
      .AddMutationConventions()
      .AddGlobalObjectIdentification() // adds "node" query and support for ID type
      // .AddDefaultNodeIdSerializer(useUrlSafeBase64: true) // and support for ID type
      .AddSubscriptionType()

      // .AddSubscriptionDiagnostics()
      .AddInMemorySubscriptions() // for test purposes only
      .AddPagingArguments()
      .ModifyRequestOptions(
        o =>
        {
          o.IncludeExceptionDetails = true;
        }
      )
      .ModifyPagingOptions(
        o =>
        {
          o.IncludeNodesField = true;
        }
      )
      .AddFiltering(
        c =>
        {
          c.AddDefaults();
          c.BindRuntimeType<string, EnrichedThisStringFilterInputType>();
          c.BindRuntimeType<bool, EnrichedThisBooleanFilterInputType>();
        }
      )
      .AddSorting()
      .AddProjections()
      .RegisterDbContextFactory<HCDemoDbContext>()
      .AddDbContextCursorPagingProvider()
      .AddInstrumentation(
        o =>
        {
          o.RequestDetails = RequestDetails.All;
          o.IncludeDocument = true;
          o.RenameRootActivity = true;
        }
      )
      .AddFileSystemOperationDocumentStorage("./PersistedOperations")
      .UseAutomaticPersistedOperationPipeline()
      .AddUploadType()
      .AddHCDemoTypes()
      .AddHCDemoServiceTypes();

    return services;
  }
}
