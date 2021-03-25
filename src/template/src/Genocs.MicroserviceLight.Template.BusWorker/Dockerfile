#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Genocs.MicroserviceLight.Template.BusWorker/Genocs.MicroserviceLight.Template.BusWorker.csproj", "src/Genocs.MicroserviceLight.Template.BusWorker/"]
COPY ["src/Genocs.MicroserviceLight.Template.Infrastructure/Genocs.MicroserviceLight.Template.Infrastructure.csproj", "src/Genocs.MicroserviceLight.Template.Infrastructure/"]
COPY ["src/Genocs.MicroserviceLight.Template.Application/Genocs.MicroserviceLight.Template.Application.csproj", "src/Genocs.MicroserviceLight.Template.Application/"]
COPY ["src/Genocs.MicroserviceLight.Template.Domain/Genocs.MicroserviceLight.Template.Domain.csproj", "src/Genocs.MicroserviceLight.Template.Domain/"]
COPY ["src/Genocs.MicroserviceLight.Template.Shared/Genocs.MicroserviceLight.Template.Shared.csproj", "src/Genocs.MicroserviceLight.Template.Shared/"]
RUN dotnet restore "src/Genocs.MicroserviceLight.Template.BusWorker/Genocs.MicroserviceLight.Template.BusWorker.csproj"
COPY . .
WORKDIR "/src/src/Genocs.MicroserviceLight.Template.BusWorker"
RUN dotnet build "Genocs.MicroserviceLight.Template.BusWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Genocs.MicroserviceLight.Template.BusWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Genocs.MicroserviceLight.Template.BusWorker.dll"]