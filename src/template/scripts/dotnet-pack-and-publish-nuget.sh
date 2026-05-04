#!/bin/bash
echo Executing after success scripts on branch $TRAVIS_BRANCH
echo Triggering NuGet package build

dotnet pack -p:NuspecFile=Genocs.Core.nuspec --no-restore -o . -p:Version=2.0.$TRAVIS_BUILD_NUMBER
dotnet pack -p:NuspecFile=Genocs.Persistence.MongoDB.nuspec --no-restore -o . -p:Version=2.0.$TRAVIS_BUILD_NUMBER

echo Uploading Genocs.Core package to NuGet using branch $TRAVIS_BRANCH

case "$TRAVIS_BRANCH" in
  "main")
    dotnet nuget push *.nupkg -k $NUGET_API_KEY -s $NUGET_SOURCE
    ;;  
esac
