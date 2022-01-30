![Clean Architecture Template](https://raw.githubusercontent.com/Genocs/clean-architecture-template/master/icon.png) .NET Microservice Template by Genocs
=========

[![Build Status](https://app.travis-ci.com/Genocs/clean-architecture-template.svg?branch=master)](https://app.travis-ci.com/Genocs/clean-architecture-template) <a href="https://www.nuget.org/packages/Genocs.CleanArchitectureTemplate/" rel="Genocs.CleanCode">![NuGet](https://buildstats.info/nuget/genocs.cleanarchitecturetemplate)</a> [![Gitter](https://img.shields.io/badge/chat-on%20gitter-blue.svg)](https://gitter.im/genocs/)

----

This is an Application Template built to help you create LOB applications. It follows the Clean Architecture Principles and built on Domain-Driven-Design. This tool is useful to increases productivity on developing your next microservices.
The template allows you to use different storage and enterprise service bus.

Storage:
- InMemoryDB (for development purpose)
- SqlServer
- MongoDB


Enterprise Service libraries:
- Particular NServiceBus
- MassTransit
- Rebus


## How to create a project

Create, build, test and run:

```sh
dotnet new -i Genocs.CleanArchitectureTemplate::{version]
dotnet new cleanarchitecture -n {MyCompany.MyProject}
cd {MyCompany.MyProject}
dotnet build src\{MyCompany.MyProject}.WebApi
dotnet build src\{MyCompany.MyProject}.BusWorker

dotnet test
cd src\{MyCompany.MyProject}.WebApi
dotnet run src\{MyCompany.MyProject}.WebApi
```


## How to build the package

To build the package run the following commands:

[Official Link](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates)


```sh
cd .\src
nuget pack
dotnet new -u Genocs.CleanArchitectureTemplate
dotnet new -i Genocs.CleanArchitectureTemplate.2.0.{version}.nupkg
dotnet new cleanarchitecture --help
dotnet new cleanarchitecture --name {MyCompany.MyProject} -d mongodb -sb particular
```


## Sample application

Run `dotnet new -i Genocs.CleanArchitecture` then try the following commands.



```sh
# Complete suite of use cases.
dotnet new cleanarchitecture --use-cases full

# Register account and get customer details.
dotnet new cleanarchitecture --use-cases basic

# Read only use cases
dotnet new cleanarchitecture --use-cases readonly
```


## Miscellaneous

How to get the list of installed templates

```sh
dotnet new -u

dotnet --list
```

### Before push on Github

The templete is built using [Travis CI](https://www.travis-ci.com/)

The CI is triggered on `master` branch

