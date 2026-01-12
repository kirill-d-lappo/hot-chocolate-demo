FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /repo

# Restore packages first for caching
COPY ./*.slnx              ./
COPY ./*.props             ./
COPY --parents **/*.csproj ./

# sharing=locked: [...] so the caches use the option sharing=locked to ensure
# sharing=locked: parallel builds using the same cache mount wait for each other
# sharing=locked: and not access the same cache files at the same time.

RUN --mount=type=cache,sharing=locked,target=/root/.nuget/packages  \
    dotnet restore

COPY . .

# Build and publish a release
RUN --mount=type=cache,sharing=locked,target=/root/.nuget/packages  \
    dotnet publish "/repo/src/HCDemo.Gql.Api" --no-restore -c Release -o /publish  \
    -p:UseAppHost=false  \
    -p:GQLSchemaGenOutput=

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
USER $APP_UID
WORKDIR /app
COPY --from=build /publish /app/

ENTRYPOINT ["dotnet", "./HCDemo.Gql.Api.dll"]
