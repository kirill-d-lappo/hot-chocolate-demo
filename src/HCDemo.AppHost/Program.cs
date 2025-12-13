using HCDemo.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("Database-Password", true);

var sqlDataPath = builder
  .AddParameter("Database-DataPath")
  .GetNonEmptyValueOrDefault("./db-data");

if (!Directory.Exists(sqlDataPath))
{
  Directory.CreateDirectory(sqlDataPath);
}

var sql = builder
  .AddSqlServer("hcdemo-sql", sqlPassword)
  .WithLifetime(ContainerLifetime.Persistent)
  .WithDataBindMount(sqlDataPath);

var db = sql.AddDatabase("HCDemo", "hcdemo");

builder
  .AddProject<Projects.HCDemo_Gql_Api>("hcdemo-gql")
  .WithReference(db)
  .WaitFor(db);

builder
  .Build()
  .Run();
