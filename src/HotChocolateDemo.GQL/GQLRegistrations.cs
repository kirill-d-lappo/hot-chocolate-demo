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
      .AddQueryConventions()
      .AddMutationConventions()
      .AddApolloFederation(GqlFederationVersion.Federation27)
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
      .AddInstrumentation()
      .AddHCDemoTypes()
      .AddHCDemoServiceTypes();

    return services;
  }
}
