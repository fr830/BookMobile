# BookServer Notes

## Run ASP.NET Core app

```docker
  docker build -t aspnetapp .
  docker run --name aspnetapp --rm -it -p 8000:80 aspnetapp
 
  docker build -t bookserver .
 
  # Here http://bookserver.domain.com:8000
  docker run --name bookserver --rm -it -p 8000:80 bookserver
  docker run --detach --name bookserver -p 8000:80 bookserver
 
  # --detach, -d  Run container in background and print container ID
  # --name Assign a name to the container
  # --rm   Automatically remove the container when it exits
  # -it    Allocate a pseudo-TTY connected to the container’s stdin, creating an interactive bash shell in the container.
  docker run --name bookserver --rm -it -p 8000:80 -e VIRTUAL_HOST=domain.com bookserver
```

## Using SSL Cert

```docker
# Run ASP.NET Core app using SSL cert
docker run --rm -it -p 8000:80 -p 8001:443 \
    -e ASPNETCORE_URLS="https://+;http://+" \
    -e ASPNETCORE_HTTPS_PORT=8001 \
    -e VIRTUAL_HOST=domain.com \
    -e VIRTUAL_PROTO=https \
    -e VIRTUAL_PORT=443 \
    -e ASPNETCORE_Kestrel__Certificates__Default__Password="password" \
    -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/domain.com.pfx \
    -v ${HOME}/.aspnet/https:/https/ mcr.microsoft.com/dotnet/core/samples:aspnetapp
```

## References

- [Dockerize a .NET Core application](https://docs.docker.com/engine/examples/dotnetcore/)
- [ASP.NET Core Docker Sample](https://github.com/dotnet/dotnet-docker/tree/master/samples/aspnetapp)
- [Hosting ASP.NET Core Images with Docker over HTTPS](https://github.com/dotnet/dotnet-docker/blob/master/samples/aspnetapp/aspnetcore-docker-https.md)
- [nginx-proxy](https://github.com/jwilder/nginx-proxy)


