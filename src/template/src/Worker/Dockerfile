#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/Worker/Host.csproj", "src/Worker/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Contracts/Contracts.csproj", "src/Contracts/"]
# COPY ["src/Contracts.NServiceBus/Contracts.NServiceBus.csproj", "src/Contracts.NServiceBus/"]

COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]

RUN dotnet restore "src/Worker/Host.csproj"
COPY . .
WORKDIR "/src/src/Worker"
RUN dotnet build "Host.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Genocs.CleanArchitecture.Template.Worker.dll"]