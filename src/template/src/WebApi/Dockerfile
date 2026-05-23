# See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Use the official .NET runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the official .NET SDK for building
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src

# Copy global build configuration files first for better layer caching
#if NServiceBus
COPY ["src/Contracts.NServiceBus/Contracts.NServiceBus.csproj", "src/Contracts.NServiceBus/"]
#endif
COPY ["Directory.Build.props", "Directory.Build.props"]
COPY ["Directory.Build.targets", "Directory.Build.targets"]
COPY ["stylecop.json", "stylecop.json"]
COPY ["dotnet.ruleset", "dotnet.ruleset"]
COPY ["global.json", "global.json"]
COPY ["NuGet.config", "NuGet.config"]

# COPY .csproj and restore as distinct layers
# This is CRITICAL: If code changes but .csproj doesn't, Docker reuses this cached layer

#if NServiceBus
COPY ["src/Contracts.NServiceBus/Contracts.NServiceBus.csproj", "src/Contracts.NServiceBus/"]
#endif
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/Contracts/Contracts.csproj", "src/Contracts/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/WebApi/Host.csproj", "src/WebApi/"]

# Restore dependencies in a separate layer for better caching
RUN dotnet restore "src/WebApi/Host.csproj"

# Copy the rest of the source code
#if NServiceBus
COPY ["src/Contracts.NServiceBus/Contracts.NServiceBus.csproj", "src/Contracts.NServiceBus/"]
#endif

COPY ["src/Application", "src/Application/"]
COPY ["src/Contracts", "src/Contracts/"]
COPY ["src/Domain", "src/Domain/"]
COPY ["src/Infrastructure", "src/Infrastructure/"]
COPY ["src/WebApi", "src/WebApi/"]


WORKDIR "/src/src/WebApi"
RUN dotnet build "Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Create final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Genocs.CleanArchitecture.Template.WebApi.dll"]