<!-- PROJECT SHIELDS -->

[![License][license-shield]][license-url]
[![Build][build-shield]][build-url]
[![Packages][package-shield]][package-url]
[![Downloads Prev][downloads-prev-shield]][downloads-prev-url]
[![Downloads][downloads-shield]][downloads-url]
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![Discord][discord-shield]][discord-url]
[![Gitter][gitter-shield]][gitter-url]
[![Twitter][twitter-shield]][twitter-url]
[![Twitterx][twitterx-shield]][twitterx-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

[license-shield]: https://img.shields.io/github/license/Genocs/clean-architecture-template?color=2da44e&style=flat-square
[license-url]: https://github.com/Genocs/clean-architecture-template/blob/main/LICENSE
[build-shield]: https://github.com/Genocs/clean-architecture-template/actions/workflows/build_and_test.yml/badge.svg?branch=main
[build-url]: https://github.com/Genocs/clean-architecture-template/actions/workflows/build_and_test.yml
[package-shield]: https://img.shields.io/badge/nuget-v.3.1.0-blue?&label=latests&logo=nuget
[package-url]: https://github.com/Genocs/clean-architecture-template/actions/workflows/build_and_test.yml
[downloads-prev-shield]: https://img.shields.io/nuget/dt/Genocs.CleanArchitectureTemplate.svg?color=2da44e&label=downloads%20prev&logo=nuget
[downloads-prev-url]: https://www.nuget.org/packages/Genocs.CleanArchitectureTemplate
[downloads-shield]: https://img.shields.io/nuget/dt/Genocs.CleanArchitecture.Template.svg?color=2da44e&label=downloads&logo=nuget
[downloads-url]: https://www.nuget.org/packages/Genocs.CleanArchitecture.Template
[contributors-shield]: https://img.shields.io/github/contributors/Genocs/clean-architecture-template.svg?style=flat-square
[contributors-url]: https://github.com/Genocs/clean-architecture-template/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/Genocs/clean-architecture-template?style=flat-square
[forks-url]: https://github.com/Genocs/clean-architecture-template/network/members
[stars-shield]: https://img.shields.io/github/stars/Genocs/clean-architecture-template.svg?style=flat-square
[stars-url]: https://img.shields.io/github/stars/Genocs/clean-architecture-template?style=flat-square
[issues-shield]: https://img.shields.io/github/issues/Genocs/clean-architecture-template?style=flat-square
[issues-url]: https://github.com/Genocs/clean-architecture-template/issues
[discord-shield]: https://img.shields.io/discord/1106846706512953385?color=%237289da&label=Discord&logo=discord&logoColor=%237289da&style=flat-square
[discord-url]: https://discord.com/invite/fWwArnkV
[gitter-shield]: https://img.shields.io/badge/chat-on%20gitter-blue.svg
[gitter-url]: https://gitter.im/genocs/
[twitter-shield]: https://img.shields.io/twitter/follow/genocs?color=1DA1F2&label=Twitter&logo=Twitter&style=flat-square
[twitter-url]: https://twitter.com/genocs
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=flat-square&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/giovanni-emanuele-nocco-b31a5169/
[twitterx-shield]: https://img.shields.io/twitter/url/https/twitter.com/genocs.svg?style=social
[twitterx-url]: https://twitter.com/genocs

<p align="center">
    <img src="./assets/genocs-library-logo.png" alt="icon">
</p>

# Genocs Clean Architecture Template

A comprehensive .NET 9 project template that follows Clean Architecture principles and Domain-Driven Design (DDD). This template helps you rapidly scaffold microservices applications with built-in support for multiple databases, message brokers, and enterprise patterns.

## 📋 Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Quick Start](#quick-start)
- [Installation](#installation)
- [Usage](#usage)
- [Template Options](#template-options)
- [Architecture Overview](#architecture-overview)
- [Examples](#examples)
- [Contributing](#contributing)
- [Support](#support)

## ✨ Features

- 🏗️ **Clean Architecture** - Domain, Application, Infrastructure, and Presentation layers
- 🎯 **Domain-Driven Design** - Rich domain models with proper separation of concerns
- 📨 **CQRS Pattern** - Command Query Responsibility Segregation with MediatR
- 🚌 **Message Brokers** - Support for RabbitMQ, Azure Service Bus, and more
- 🗃️ **Multiple Databases** - SQL Server, MongoDB, Redis integration
- 🔍 **Observability** - OpenTelemetry tracing and monitoring
- 🐳 **Containerization** - Docker and Kubernetes ready
- ⚡ **Background Services** - Worker services for async processing

### Databases supported:

- InMemoryDB (for development purpose)
- SqlServer
- MongoDB

### Enterprise Service Bus libraries supported:

- Particular NServiceBus
- MassTransit
- Rebus

## 📋 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (latest version)
- **IDE** (choose one):
  - [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (recommended)
  - [Visual Studio Code](https://code.visualstudio.com/) with C# extension
  - [JetBrains Rider](https://www.jetbrains.com/rider/)
- **Optional for development**:
  - [Docker Desktop](https://www.docker.com/products/docker-desktop) for containerization
  - MongoDB, SQL Server, or Redis (if not using Docker)

## 🚀 Quick Start

### Install the Template

```bash
# Install the latest version
dotnet new install Genocs.CleanArchitecture.Template

# Or install a specific version
dotnet new install Genocs.CleanArchitecture.Template::3.1.0

# Create a new microservice
dotnet new cleanarchitecture --name "MyCompany.OrderService" --database mongodb --servicebus masstransit

# Navigate to the project directory
cd MyCompany.OrderService

# Build and run
dotnet build
dotnet test
dotnet run --project src/MyCompany.OrderService.WebApi

# View all available options
dotnet new cleanarchitecture --help

# Example with custom options
dotnet new cleanarchitecture \
  --name "Acme.InventoryService" \
  --database sqlserver \
  --servicebus particular \
  --use-cases full
```

## 🏗️ Architecture Overview

The template generates a solution with the following structure:

```pl
src/
├── Domain/ # Core business logic and entities
├── Application/ # Use cases and application services
├── Infrastructure/ # Data access and external services
├── WebApi/ # REST API controllers and middleware
├── Worker/ # Background services and message handlers
├── Shared/ # Cross-cutting concerns and DTOs
└── Contracts/ # API contracts and events
```

### Key Components

- **Domain Layer**: Entities, value objects, domain services
- **Application Layer**: CQRS handlers, interfaces, DTOs
- **Infrastructure Layer**: Repositories, message brokers, databases
- **Presentation Layer**: Controllers, middleware, API documentation

## 🔧 Development Workflow

### Local Development

```bash
# Start infrastructure services
docker-compose -f docker-infrastructure/docker-compose-infrastructure.yml up -d

# Run the API
dotnet run --project src/YourProject.WebApi

# Run the Worker
dotnet run --project src/YourProject.Worker

# Run all tests
dotnet test

# Run specific test projects
dotnet test src/UnitTests
dotnet test src/IntegrationTests
dotnet test src/AcceptanceTests
```

## 💬 Community & Support

### Get Help

- 💬 [Discord Community](https://discord.com/invite/fWwArnkV)
- 📖 [Documentation](https://genocs-blog.netlify.app/library/)
- 🐛 [Report Issues](https://github.com/Genocs/clean-architecture-template/issues)

### Stay Connected

- 🐦 [Twitter @genocs](https://twitter.com/genocs)
- 📺 [YouTube Channel](https://youtube.com/c/genocs)
- 💼 [LinkedIn](https://www.linkedin.com/in/giovanni-emanuele-nocco-b31a5169/)

### Show Your Support

- ⭐ Star this repository
- 🔄 Share with your team
- ☕ [Buy me a coffee](https://www.buymeacoffee.com/genocs)

## 🔧 Troubleshooting

### Common Issues

**Template installation fails**

```bash
# Uninstall old version first
dotnet new uninstall Genocs.CleanArchitecture.Template
dotnet new install Genocs.CleanArchitecture.Template

# Restore packages
dotnet restore
dotnet build

# To clone the repository
git clone https://github.com/Genocs/clean-architecture-template
# To build the nuget package
nuget pack ./src/Package.Template.nuspec -NoDefaultExcludes -OutputDirectory ./out -Version 3.1.0

```


For more details on getting started, [read the documentation](https://genocs-blog.netlify.app/library/)

Please check the [documentation](https://learn.microsoft.com/en-us/visualstudio/ide/how-to-locate-and-organize-project-and-item-templates?view=vs-2022) for more details.

## How to build the package

To build the package run the following commands:

[Official Link](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates)

```bash
cd ./src
nuget pack
dotnet new u Genocs.CleanArchitecture.Template
dotnet new i ./out/Genocs.CleanArchitecture.Template.3.1.0.nupkg
dotnet new cleanarchitecture --help
dotnet new cleanarchitecture --name {MyCompany.MyProject} -d mongodb -sb particular
```

## Sample application

Run `dotnet new -i Genocs.CleanArchitecture.Template` then try the following commands.

```bash
# Complete suite of use cases.
dotnet new cleanarchitecture --use-cases full

# Register account and get customer details.
dotnet new cleanarchitecture --use-cases basic

# Read only use cases
dotnet new cleanarchitecture --use-cases readonly
```

## Miscellaneous

Useful commands:

```bash
# How to get the list of installed templates
dotnet new -u

dotnet --list
```

## Changelogs

View Complete [Changelogs](https://github.com/Genocs/clean-architecture-template/blob/main/CHANGELOGS.md).

## License

This project is licensed with the [MIT license](LICENSE).

## Community

- Discord [@genocs](https://discord.com/invite/fWwArnkV)
- Facebook Page [@genocs](https://facebook.com/Genocs)
- Youtube Channel [@genocs](https://youtube.com/c/genocs)

## Code Contributors

This project exists thanks to all the people who contribute. [Submit your PR and join the team!](CONTRIBUTING.md)

[![genocs contributors](https://contrib.rocks/image?repo=Genocs/clean-architecture-template "genocs contributors")](https://github.com/Genocs/clean-architecture-template/graphs/contributors)

## Financial Contributors

Become a financial contributor and help me sustain the project.

**Support the Project** on [Opencollective](https://opencollective.com/genocs)

<a href="https://opencollective.com/genocs"><img src="https://opencollective.com/genocs/individuals.svg?width=890"></a>

## Acknowledgements
