#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE $PORT

FROM mcr.microsoft.com/dotnet/sdk:3.1-alpine AS build
WORKDIR /src
COPY ./ ./
RUN dotnet restore "web.api.demarcacao.gestao.usuarios.Endpoint/web.api.demarcacao.gestao.usuarios.Endpoint.csproj"

RUN dotnet build "web.api.demarcacao.gestao.usuarios.Endpoint/web.api.demarcacao.gestao.usuarios.Endpoint.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "web.api.demarcacao.gestao.usuarios.Endpoint/web.api.demarcacao.gestao.usuarios.Endpoint.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

#CMD ASPNETCORE_URLS=http://*:$PORT dotnet web.api.demarcacao.usuarios.Endpoint.dll

ENTRYPOINT ["dotnet", "web.api.demarcacao.gestao.usuarios.Endpoint.dll"]