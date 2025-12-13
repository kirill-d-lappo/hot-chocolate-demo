using HotChocolate.Diagnostics;
using HotChocolate.Execution;
using HCDemo.Persistence;

namespace HCDemo.Gql.Api;

public static class GqlRegistrations
{
  public static IServiceCollection AddGqlServices(this IServiceCollection services)
  {
    var gqlBuilder = services

      // Note [2024-11-25 klappo] costs calculation is disabled for a while
      .AddGraphQLServer(disableDefaultSecurity: true)
      .InitializeOnStartup((executor, ct) =>
        {
          var request = OperationRequestBuilder
            .New()
            .SetDocument("query get_all_users{ allUsers { nodes {   id   userName }  } }")
            .SetOperationName("get_all_users")
            .MarkAsWarmupRequest()
            .Build();

          return executor.ExecuteAsync(request, ct);
        }
      )
      .ModifyOptions(o =>
        {
          o.SortFieldsByName = true;
        }
      )
      .AddQueryType()
      .AddQueryConventions()
      .AddMutationType()
      .AddMutationConventions()

      // .AddQueryFieldToMutationPayloads()
      .AddSubscriptionType()
      .AddSubscriptionDiagnostics()
      .AddInMemorySubscriptions() // for test purposes only
      .AddPagingArguments()

      // Note [2025-02-19 klappo] node{} and global ID usage
      // Note [2025-02-19 klappo] enable at least one NodeResolver on a type to add it to Node type set
      // .AddGlobalObjectIdentification() // adds "node" query and support for ID type
      // .AddDefaultNodeIdSerializer(useUrlSafeBase64: true) // and support for ID type
      .ModifyRequestOptions(o =>
        {
          o.IncludeExceptionDetails = true;
        }
      )
      .ModifyPagingOptions(o =>
        {
          o.IncludeNodesField = true;
          o.DefaultPageSize = 5;
          o.MaxPageSize = 10;
        }
      )
      .AddFiltering()
      .AddSorting()
      .AddProjections()
      .RegisterDbContextFactory<HCDemoDbContext>()
      .AddDbContextCursorPagingProvider()
      .AddInstrumentation(o =>
        {
          o.RequestDetails = RequestDetails.All;
          o.IncludeDocument = true;
          o.RenameRootActivity = true;
        }
      )
      .AddFileSystemOperationDocumentStorage("./PersistedOperations")
      .UseAutomaticPersistedOperationPipeline()
      .AddErrorLogging();

    gqlBuilder
      .AddUploadType()
      .AddTimeSpan();

    gqlBuilder
      .AddHCDemoGqlTypes()
      .AddHCDemoServiceTypes();

    gqlBuilder.AddHCDemoUserTypes();

    // Note [2025-01-23 klappo] that hash provider is used in persisted operations
    // FixMe [2025-01-23 klappo] figure it out how it is used in HC pipeline, atm the pipeline just grabs file by its name
    // services.AddMD5DocumentHashProvider();

    return services;
  }
}
