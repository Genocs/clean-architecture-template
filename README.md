![Clean Architecture Template](https://raw.githubusercontent.com/genocs/clean-architecture-template/master/images/genocs-icon.png) .NET Microservice Template by Genocs
=========
Service Template to help you build evolvable and maintainable applications.
It follows the Clean Architecture Principles and built on Domain-Driven Design.
This tool increases productivity on developing your next microservices.

## Generate your own awesome Back-end!
<a href="https://www.nuget.org/packages/Genocs.CleanArchitectureTemplate/" rel="Genocs.CleanCode">![NuGet](https://buildstats.info/nuget/genocs.cleanarchitecturetemplate)</a> [![Build Status](https://travis-ci.org/genocs/clean-architecture-template.svg?branch=master)](https://travis-ci.org/genocs/clean-architecture-template) [![Gitter](https://img.shields.io/badge/chat-on%20gitter-blue.svg)](https://gitter.im/genocs/)

To generate your own awesome .NET Back-end simple run:

```sh
dotnet new -i Genocs.CleanArchitectureTemplate::0.1.11
dotnet new cleanarchitecture
```

## Clean Architecture

Based on [Clean Architecture](https://github.com/genocs/clean-architecture).


## Build the packages

```sh
cd .\src
nuget pack 
```


## Sample applications

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