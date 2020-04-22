#!/bin/bash
rm -rf samples
mkdir samples
pushd samples
dotnet new cleanarchitecture -n "MyCompany.MyProject"
#dotnet new cleanarchitecture --use-cases basic -n MyBasic
#dotnet new cleanarchitecture --use-cases readonly -n MyReadOnly
popd