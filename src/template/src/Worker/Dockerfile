#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Genocs.CleanArchitecture.Template.BusWorker/Genocs.CleanArchitecture.Template.BusWorker.csproj", "src/Genocs.CleanArchitecture.Template.BusWorker/"]
COPY ["src/Genocs.CleanArchitecture.Template.Infrastructure/Genocs.CleanArchitecture.Template.Infrastructure.csproj", "src/Genocs.CleanArchitecture.Template.Infrastructure/"]
COPY ["src/Genocs.CleanArchitecture.Template.Application/Genocs.CleanArchitecture.Template.Application.csproj", "src/Genocs.CleanArchitecture.Template.Application/"]
COPY ["src/Genocs.CleanArchitecture.Template.Domain/Genocs.CleanArchitecture.Template.Domain.csproj", "src/Genocs.CleanArchitecture.Template.Domain/"]
COPY ["src/Genocs.CleanArchitecture.Template.Shared/Genocs.CleanArchitecture.Template.Shared.csproj", "src/Genocs.CleanArchitecture.Template.Shared/"]
COPY ["src/Genocs.CleanArchitecture.Template.Shared.Particular/Genocs.CleanArchitecture.Template.Shared.Particular.csproj", "src/Genocs.CleanArchitecture.Template.Shared.Particular/"]
RUN dotnet restore "src/Genocs.CleanArchitecture.Template.BusWorker/Genocs.CleanArchitecture.Template.BusWorker.csproj"
COPY . .
WORKDIR "/src/src/Genocs.CleanArchitecture.Template.BusWorker"
RUN dotnet build "Genocs.CleanArchitecture.Template.BusWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Genocs.CleanArchitecture.Template.BusWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Genocs.CleanArchitecture.Template.BusWorker.dll"]