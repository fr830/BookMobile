# Dockerize a .NET Core application
# https://docs.docker.com/engine/examples/dotnetcore/

# ASP.NET Core Docker Sample
# https://github.com/dotnet/dotnet-docker/tree/master/samples/aspnetapp

# Hosting ASP.NET Core Images with Docker over HTTPS
# https://github.com/dotnet/dotnet-docker/blob/master/samples/aspnetapp/aspnetcore-docker-https.md

# nginx-proxy
# https://github.com/jwilder/nginx-proxy

# docker build -t aspnetapp .
# docker run --name aspnetapp --rm -it -p 8000:80 aspnetapp

# docker build -t bookserver .

# docker run --name bookserver --rm -it -p 8000:80 bookserver
# docker run --detach --name bookserver -p 8000:80 bookserver

# --name Assign a name to the container
# --rm   Automatically remove the container when it exits
# -it    Allocate a pseudo-TTY connected to the container’s stdin, creating an interactive bash shell in the container.
# docker run --name bookserver --rm -it -p 8000:80 -e VIRTUAL_HOST=bookserver.cogentcoder.com bookserver

# --detach, -d  Run container in background and print container ID
# docker run --detach --name bookserver -it -p 8000:80 -e VIRTUAL_HOST=bookserver.cogentcoder.com bookserver

# Run using SSL cert
# docker run --rm -it -p 8000:80 -p 8001:443 \
#     -e ASPNETCORE_URLS="https://+;http://+" \
#     -e ASPNETCORE_HTTPS_PORT=8001 \
#     -e VIRTUAL_HOST=dev.cogentcoder.com \
#     -e VIRTUAL_PROTO=https \
#     -e VIRTUAL_PORT=443 \
#     -e ASPNETCORE_Kestrel__Certificates__Default__Password="w{pSiho6m98WoejqZggBnez2" \
#     -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/dev.cogentcoder.com.pfx \
#     -v ${HOME}/.aspnet/https:/https/ mcr.microsoft.com/dotnet/core/samples:aspnetapp

FROM microsoft/dotnet:sdk AS build
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
FROM microsoft/dotnet:aspnetcore-runtime
# FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /app/BookServer/out ./
ENTRYPOINT ["dotnet", "BookServer.dll"]
