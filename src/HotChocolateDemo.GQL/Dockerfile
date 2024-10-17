FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /repo
#RUN apk add clang binutils musl-dev build-base zlib-static
#RUN apk add cmake
#RUN apk add openssl-dev openssl-libs-static openssl
# Copy everything
COPY ./src/HotChocolateDemo.GQL/HotChocolateDemo.GQL.csproj ./src/HotChocolateDemo.GQL/HotChocolateDemo.GQL.csproj
COPY ./src/HotChocolateDemo.Persistence/HotChocolateDemo.Persistence.csproj ./src/HotChocolateDemo.Persistence/HotChocolateDemo.Persistence.csproj
COPY ./src/HotChocolateDemo.Services/HotChocolateDemo.Services.csproj ./src/HotChocolateDemo.Services/HotChocolateDemo.Services.csproj
COPY ./Directory.Build.props ./Directory.Build.props
COPY ./Directory.Packages.props ./Directory.Packages.props

# Restore as distinct layers
RUN dotnet restore "/repo/src/HotChocolateDemo.GQL"

COPY . ./
# Build and publish a release
RUN dotnet publish "/repo/src/HotChocolateDemo.GQL" --no-restore -c Release -o /publish

FROM alpine AS build2

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app
COPY --from=build /publish /app/
#ENV ASPNETCORE_HTTP_PORTS=8080 DOTNET_RUNNING_IN_CONTAINER=true
#EXPOSE 8080
ENTRYPOINT ["/app/HotChocolateDemo.GQL"]
#CMD ["/bin/busybox", "sleep", "5"]
#ENTRYPOINT ["sleep", "300"]
