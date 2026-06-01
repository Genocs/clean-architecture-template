#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
dotnet run --project src/WebApi/Host.csproj
