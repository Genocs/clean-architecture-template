#!/bin/bash
MYGET_ENV=""
case "$TRAVIS_BRANCH" in
  "develop")
    MYGET_ENV="-dev"
    ;;
esac

dotnet build ../src/Genocs.MicroserviceLight.Template.WebApi -c release
dotnet build ../src/Genocs.MicroserviceLight.Template.BusWorker -c release