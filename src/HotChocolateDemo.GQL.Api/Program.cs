using FluentValidation;
using HotChocolateDemo.GQL.Api.Filters;
using HotChocolateDemo.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
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

builder.Services.AddHCDemoPersistence();

builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

var app = builder.Build();

if (!IsGraphQLCommand(args))
{
  await MigrateDatabase<HCDemoDbContext>(app);
}

app.MapGraphQL();

app.RunWithGraphQLCommands(args);

return;

static async Task MigrateDatabase<TDbContext>(IHost app)
  where TDbContext : DbContext
{
  using var scope = app.Services
    .CreateScope();

  await using var dbContext = await scope
    .ServiceProvider
    .GetRequiredService<IDbContextFactory<TDbContext>>()
    .CreateDbContextAsync();

  await dbContext.Database.MigrateAsync();
}

static bool IsGraphQLCommand(string[] args)
{
  return args is ["schema", ..,];
}
