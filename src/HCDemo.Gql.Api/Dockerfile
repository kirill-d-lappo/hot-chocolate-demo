#syntax=docker/dockerfile:1-labs

# enable --parents parameter: https://docs.docker.com/reference/dockerfile/#copy---parents

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /repo

# Restore packages first for caching
COPY ./*.slnx              ./
COPY ./*.props             ./
COPY --parents **/*.csproj ./
RUN dotnet restore

COPY . .

# Build and publish a release
RUN dotnet publish "/repo/src/HCDemo.Gql" --no-restore -c Release -o /publish

FROM alpine AS build2

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine
WORKDIR /app
COPY --from=build /publish /app/
#ENV ASPNETCORE_HTTP_PORTS=8080 DOTNET_RUNNING_IN_CONTAINER=true
#EXPOSE 8080
ENTRYPOINT ["/app/HCDemo.GQL"]
#CMD ["/bin/busybox", "sleep", "5"]
#ENTRYPOINT ["sleep", "300"]
