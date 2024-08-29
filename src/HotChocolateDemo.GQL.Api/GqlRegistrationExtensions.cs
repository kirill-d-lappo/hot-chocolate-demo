using HotChocolateDemo.GQL.Api.Filters;
using HotChocolateDemo.Persistence;

namespace HotChocolateDemo.GQL.Api;

public static class GqlRegistrationExtensions
{
  public static IServiceCollection AddGqlServices(this IServiceCollection services)
  {
    services
      .AddGraphQLServer()
      .AddApolloFederation(GqlFederationVersion.Federation25) // hotchoclate package
      // .AddApolloFederationV2(ApolloFederationVersion.FEDERATION_25) // apollo package
      .ModifyOptions(o =>
      {
        o.SortFieldsByName = true;
      })
      .AddQueryType()
      .AddQueryConventions()
      .AddMutationType()
      .AddMutationConventions()
      .AddPagingArguments()
      .ModifyPagingOptions(o =>
      {
        o.IncludeNodesField = true;
      })
      .AddFiltering(c =>
      {
        c.AddDefaults();
        c.BindRuntimeType<string, EnrichedThisStringFilterInputType>();
        c.BindRuntimeType<bool, EnrichedThisBooleanFilterInputType>();
      })
      .AddSorting()
      .AddProjections()
      .AddHCDemoTypes()
      .RegisterDbContextFactory<HCDemoDbContext>()
      .AddDbContextCursorPagingProvider()
      ;

    return services;
  }
}
