#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Genocs.CleanArchitecture.Template.WebApi/Genocs.CleanArchitecture.Template.WebApi.csproj", "src/Genocs.CleanArchitecture.Template.WebApi/"]
COPY ["src/Genocs.CleanArchitecture.Template.Application/Genocs.CleanArchitecture.Template.Application.csproj", "src/Genocs.CleanArchitecture.Template.Application/"]
COPY ["src/Genocs.CleanArchitecture.Template.Domain/Genocs.CleanArchitecture.Template.Domain.csproj", "src/Genocs.CleanArchitecture.Template.Domain/"]
COPY ["src/Genocs.CleanArchitecture.Template.Shared/Genocs.CleanArchitecture.Template.Shared.csproj", "src/Genocs.CleanArchitecture.Template.Shared/"]
COPY ["src/Genocs.CleanArchitecture.Template.Infrastructure/Genocs.CleanArchitecture.Template.Infrastructure.csproj", "src/Genocs.CleanArchitecture.Template.Infrastructure/"]
RUN dotnet restore "src/Genocs.CleanArchitecture.Template.WebApi/Genocs.CleanArchitecture.Template.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Genocs.CleanArchitecture.Template.WebApi"
RUN dotnet build "Genocs.CleanArchitecture.Template.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Genocs.CleanArchitecture.Template.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Genocs.CleanArchitecture.Template.WebApi.dll"]