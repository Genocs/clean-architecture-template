#!/bin/bash
rm -rf samples
mkdir samples
pushd samples
dotnet new genocs --use-cases full -n MyFull
dotnet new genocs --use-cases basic -n MyBasic
dotnet new genocs --use-cases readonly -n MyReadOnly
popd