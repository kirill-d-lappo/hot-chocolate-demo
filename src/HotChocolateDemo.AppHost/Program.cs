var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("SqlServerPassword", true);
var sql = builder
  .AddSqlServer("hcdemo-sql", sqlPassword)
  .WithLifetime(ContainerLifetime.Persistent)
  .WithDataBindMount(@"D:\sql-server\hcdemo-data");

var db = sql.AddDatabase("HCDemo", "hcdemo");

builder
  .AddProject<Projects.HotChocolateDemo_GQL>("hcdemo-gql")
  .WithReference(db)
  .WaitFor(db);

builder
  .Build()
  .Run();
