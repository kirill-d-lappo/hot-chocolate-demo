using HotChocolate.Diagnostics;
using HotChocolateDemo.GQL.Filters;
using HotChocolateDemo.Persistence;

namespace HotChocolateDemo.GQL;

public static class GQLRegistrations
{
  public static IServiceCollection AddGqlServices(this IServiceCollection services)
  {
    services

      // Note [2024-11-25 klappo] costs calculation is disabled for a while
      .AddGraphQLServer(disableDefaultSecurity: true)
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
      .AddUploadType()
      .AddHCDemoTypes()
      .AddHCDemoServiceTypes();

    return services;
  }
}
