![Clean Architecture Template](https://raw.githubusercontent.com/genocs/clean-architecture-template/master/images/genocs-icon.png) .NET Microservice Template by Genocs
=========
This is a Service Template to help you build evolvable and maintainable applications.
It follows the Clean Architecture Principles and built on Domain-Driven-Design.
This tool is usefull to increases productivity on developing your next microservices.

----
[![Build Status](https://travis-ci.org/genocs/clean-architecture-template.svg?branch=master)](https://travis-ci.org/genocs/clean-architecture-template) <a href="https://www.nuget.org/packages/Genocs.CleanArchitectureTemplate/" rel="Genocs.CleanCode">![NuGet](https://buildstats.info/nuget/genocs.cleanarchitecturetemplate)</a> [![Gitter](https://img.shields.io/badge/chat-on%20gitter-blue.svg)](https://gitter.im/genocs/)

## How to create a project

To generate your own Back-end project simple run:

```sh
dotnet new -i Genocs.CleanArchitectureTemplate::0.1.13
dotnet new cleanarchitecture -n {MyCompany.MyProject}
cd {MyCompany.MyProject}
dotnet build
dotnet test
cd src\{MyCompany.MyProject}.WebApi
dotnet run
```


## How to build the package

To build the packagerun the following commands:

```sh
cd .\src
nuget pack 
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
