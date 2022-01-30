#!/bin/bash
echo Executing after success scripts on branch $TRAVIS_BRANCH
echo Triggering NuGet package build

dotnet pack ./src/Genocs.QRCodeLibrary/Genocs.QRCodeLibrary.csproj -o . -p:Version=2.0.$TRAVIS_BUILD_NUMBER

echo Uploading Genocs.CleanArchitectureTemplate package to NuGet using branch $TRAVIS_BRANCH

case "$TRAVIS_BRANCH" in
  "master")
    dotnet nuget push *.nupkg -k $NUGET_API_KEY -s $NUGET_SOURCE
    ;;
esac
