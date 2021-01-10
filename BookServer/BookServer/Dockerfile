#
# Dockerfile
#
FROM microsoft/dotnet:sdk AS build
# FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app

# Delete temp files
RUN rm -rf BookServer/bin/* BookServer/obj/*


# copy csproj and restore as distinct layers
COPY *.sln .
COPY BookServer/*.csproj ./BookServer/
RUN dotnet restore

# copy everything else and build app
COPY BookServer/. ./BookServer/
WORKDIR /app/BookServer
RUN dotnet publish -c Release -o out


# Build runtime image
# FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build /app/BookServer/out ./
ENTRYPOINT ["dotnet", "BookServer.dll"]
