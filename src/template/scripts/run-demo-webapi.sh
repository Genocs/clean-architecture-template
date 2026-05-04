#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
dotnet run --project src/demo/WebApi/Host.csproj
