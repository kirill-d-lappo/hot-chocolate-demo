using HotChocolateDemo.GQL.Filters;
using HotChocolateDemo.Persistence;

namespace HotChocolateDemo.GQL;

public static class GqlRegistrationExtensions
{
  public static IServiceCollection AddGqlServices(this IServiceCollection services)
  {
    services

      // Note [2024-11-25 klappo] costs calculation is disabled for a while
      .AddGraphQLServer(disableDefaultSecurity: true)
      .AddApolloFederation(GqlFederationVersion.Federation27) // HotChocolate package
      // .AddApolloFederationV2(ApolloFederationVersion.FEDERATION_25) // Apollo package
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
      .AddHCDemoTypes();

    return services;
  }
}
