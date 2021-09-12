![Clean Architecture Template](https://raw.githubusercontent.com/genocs/clean-architecture-template/master/images/genocs-icon.png) .NET Microservice Template by Genocs
=========
This is a Application Template to help you build LOB applications.
It follows the Clean Architecture Principles and built on Domain-Driven-Design.
This tool is usefull to increases productivity on developing your next microservices.

The template allow you to use different storage and enterprice service bus.

Allowed storage are:
- InMemmoryDB (for development pourpose)
- SqlServer
- MongoDB

Managed Enterprise service bus are:
- Particular NServiceBus
- MassTransit
- Rebus

----

[![Build Status](https://travis-ci.com/genocs/clean-architecture-template.svg?branch=master)](https://travis-ci.com/genocs/clean-architecture-template) <a href="https://www.nuget.org/packages/Genocs.CleanArchitectureTemplate/" rel="Genocs.CleanCode">![NuGet](https://buildstats.info/nuget/genocs.cleanarchitecturetemplate)</a> [![Gitter](https://img.shields.io/badge/chat-on%20gitter-blue.svg)](https://gitter.im/genocs/)


## How to create a project

To generate your own Back-end project run:

```sh
dotnet new -i Genocs.CleanArchitectureTemplate::1.8.0
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
dotnet new -i Genocs.CleanArchitectureTemplate.1.8.0.nupkg
dotnet new cleanarchitecture --help
dotnet new cleanarchitecture --name {MyCompany.MyProject} -d mongodb -sb particular
```


## Sample application

Run `dotnet new -i Genocs.CleanArchitecture` then try the following commands.

Complete suite of use cases.

```sh
dotnet new cleanarchitecture --use-cases full
```

Register account and get customer details.

```sh
dotnet new cleanarchitecture --use-cases basic
```

Read only use cases

```sh
dotnet new cleanarchitecture --use-cases readonly
```


## Miscellaneous

How to get the list of installed templates

```sh
dotnet new -u
```

### Before push on Github

The templete is built using [Travis CI](https://www.travis-ci.com/)

The CI is triggered on `master` branch

Check the `./src/Genocs.CleanArchitectureTemplate.nuspec` and update the node with the correct version. 

Keep in mind: NuGet fail to deploy the package in case of version mistake.
``` xml
<version>1.8.0</version>
```

