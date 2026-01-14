# How to Run HCDemo.GqlApi

## Prerequisites

- .NET 10 SDK
- SQL Server running on `localhost:1434`

## Run the Project

```bash
dotnet run --project src/HCDemo.Gql.Api
```

## Connection String

The application expects a SQL Server database with the following default connection:

- Server: `localhost, 1434`
- Database: `hcdemo`
- User: `hcdemo`
- Password: `hc1!demo`

## Local Environment with Docker Compose

The `compose/` folder contains Docker Compose configuration to start a local environment with SQL Server and the HCDemo API.

The commands must be executed from the project root folder.

It takes time for the SQL Server and Api server to start and initialize, wait until docker reports success of failure at startup.

```powershell
# Start or Update the environment
./compose/up.ps1

# Remove the environment
./compose/down.ps1
```

Services:

- **mssql**: SQL Server 2025 on port `1434`
- **mssql-init**: Initializes the database schema
- **hc-demo**: HCDemo API on port `8095`
