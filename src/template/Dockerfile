#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/Genocs.MicroserviceLight.Template.WebApi/Genocs.MicroserviceLight.Template.WebApi.csproj", "src/Genocs.MicroserviceLight.Template.WebApi/"]
COPY ["src/Genocs.MicroserviceLight.Template.Application/Genocs.MicroserviceLight.Template.Application.csproj", "src/Genocs.MicroserviceLight.Template.Application/"]
COPY ["src/Genocs.MicroserviceLight.Template.Domain/Genocs.MicroserviceLight.Template.Domain.csproj", "src/Genocs.MicroserviceLight.Template.Domain/"]
COPY ["src/Genocs.MicroserviceLight.Template.Shared/Genocs.MicroserviceLight.Template.Shared.csproj", "src/Genocs.MicroserviceLight.Template.Shared/"]
COPY ["src/Genocs.MicroserviceLight.Template.Infrastructure/Genocs.MicroserviceLight.Template.Infrastructure.csproj", "src/Genocs.MicroserviceLight.Template.Infrastructure/"]
RUN dotnet restore "src/Genocs.MicroserviceLight.Template.WebApi/Genocs.MicroserviceLight.Template.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Genocs.MicroserviceLight.Template.WebApi"
RUN dotnet build "Genocs.MicroserviceLight.Template.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Genocs.MicroserviceLight.Template.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Genocs.MicroserviceLight.Template.WebApi.dll"]