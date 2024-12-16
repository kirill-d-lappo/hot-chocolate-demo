var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.HotChocolateDemo_GQL>("hcdemo-gql");

builder
  .Build()
  .Run();
