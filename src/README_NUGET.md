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
[package-shield]: https://img.shields.io/badge/nuget-v.4.0.1-blue?&label=latests&logo=nuget
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

[![Exagonal Architecture](https://raw.githubusercontent.com/Genocs/clean-architecture-template/main/assets/exagonal-architecture.png "Exagonal Architecture")](https://github.com/Genocs/clean-architecture-template)

# Genocs Clean Architecture Template

A comprehensive .NET 9 project template that follows Clean Architecture principles and Domain-Driven Design (DDD). This template helps you rapidly scaffold microservices applications with built-in support for multiple databases, message brokers, and enterprise patterns.

## ✨ Features

- 🏗️ **Clean Architecture** - Domain, Application, Infrastructure, and Presentation layers
- 🎯 **Domain-Driven Design** - Rich domain models with proper separation of concerns
- 📨 **CQRS Pattern** - Command Query Responsibility Segregation with MediatR
- 🚌 **Message Brokers** - Support for RabbitMQ, Azure Service Bus, and more
- 🗃️ **Multiple Databases** - SQL Server, MongoDB, Redis integration
- 🔍 **Observability** - OpenTelemetry tracing and monitoring
- 🐳 **Containerization** - Docker and Kubernetes ready
- ⚡ **Background Services** - Worker services for async processing
- 🧪 **Comprehensive Testing** - Unit, Integration, and Acceptance tests

## 📋 Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Quick Start](#quick-start)
- [Template Options](#template-options)
- [Architecture Overview](#architecture-overview)
- [Development Workflow](#development-workflow)
- [Examples](#examples)
- [Troubleshooting](#troubleshooting)
- [Community & Support](#community--support)
- [Contributing](#contributing)
- [License](#license)

## 📋 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (latest version)
- **IDE** (choose one):
  - [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (recommended)
  - [Visual Studio Code](https://code.visualstudio.com/) with C# extension
  - [JetBrains Rider](https://www.jetbrains.com/rider/)
- **Optional for development**:
  - [Docker Desktop](https://www.docker.com/products/docker-desktop) for containerization
  - MongoDB, SQL Server

## 🚀 Quick Start

### Install the Template

```bash
# Install the latest version
dotnet new install Genocs.CleanArchitecture.Template

# Or install a specific version
dotnet new install Genocs.CleanArchitecture.Template::4.0.1

# View all available options
dotnet new cleanarchitecture --help

# Example with custom options
dotnet new cleanarchitecture \
  --name "CompanyName.ServiceName" \
  --database inmemory \
  --service-bus rebus \
  --use-cases full
```

## 🏗️ Architecture Overview

The template generates a solution with the following structure:

```pl
src/
├── AcceptanceTests/ # Acceptance Tests
├── Application/ # Use cases and application services
├── Contracts/ # API and ServiceBus contracts and commansd, events and messages
├── Contracts.NServiceBus/ # API and ServiceBus contracts and commansd, events and messages, used by NServiceBus
├── Domain/ # Core business logic and entities
├── Infrastructure/ # Data access and external services
├── IntegrationTests/ # Integration Tests
├── UnitTests/ # Unit Tests
├── WebApi/ # REST API controllers and middleware
├── Worker/ # Background services and message handlers
└── Contracts/ # API contracts and events
```

### Key Components

- **Domain Layer**: Entities, value objects, domain services
- **Application Layer**: CQRS handlers, interfaces, DTOs
- **Infrastructure Layer**: Repositories, message brokers, databases
- **Presentation Layer**: Controllers, middleware, API documentation

## How to build and install the template locally

To build the package run the following commands:

[custom-templates](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates)

[dotnet-templating](https://github.com/dotnet/templating)

```bash
# To clone the repository
git clone https://github.com/Genocs/clean-architecture-template
cd clean-architecture-template

# To pack and install the template
dotnet pack ./src/Package.Template.csproj -p:PackageVersion=4.0.1 --configuration Release --output ./out

dotnet new install ./out/Genocs.CleanArchitecture.Template.4.0.1.nupkg
dotnet new cleanarchitecture --help

# To uninstall the template
dotnet new uninstall Genocs.CleanArchitecture.Template

# Example of creating a new project with InMemory database and Rebus as service bus
dotnet new cleanarchitecture --name {CompanyName.ServiceName} -da inmemory -sb rebus
```

### Miscellaneous

Useful commands:

```bash
# How to get the list of installed templates
dotnet new -u

# How to get the list of templates
dotnet new list
```

## 🔧 Development Workflow

### Local Development

```bash
# Start infrastructure services
docker-compose -f ./infrastructure/docker/docker-compose-infrastructure.yml up -d

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

[![buy-me-a-coffee](https://raw.githubusercontent.com/Genocs/clean-architecture-template/main/assets/buy-me-a-coffee.png "buy me a coffee")](https://www.buymeacoffee.com/genocs)

## 🔧 Troubleshooting

### Common Issues

For more details on getting started, [read the documentation](https://genocs-blog.netlify.app/library/)

Please check the [documentation](https://learn.microsoft.com/en-us/visualstudio/ide/how-to-locate-and-organize-project-and-item-templates?view=vs-2022) for more details.

## Changelogs

View Complete [Changelogs](https://github.com/Genocs/clean-architecture-template/blob/main/CHANGELOG.md).

## License

This project is licensed with the [MIT license](LICENSE).

## Community

- Discord [@genocs](https://discord.com/invite/fWwArnkV)
- Facebook Page [@genocs](https://facebook.com/Genocs)
- Youtube Channel [@genocs](https://youtube.com/c/genocs)

## Financial Contributors

Become a financial contributor and help me sustain the project.

**Support the Project** on [Opencollective](https://opencollective.com/genocs)

<a href="https://opencollective.com/genocs"><img src="https://opencollective.com/genocs/individuals.svg?width=890"></a>

## Acknowledgements

## ⚙️ Template Options

| Option         | Description         | Values                               | Default       |
| -------------- | ------------------- | ------------------------------------ | ------------- |
| `--name`       | Project name        | `{Company.Project.Service}`          | Required      |
| `--database`   | Database provider   | `inmemory`, `sqlserver`, `mongodb`   | `inmemory`    |
| `--servicebus` | Message broker      | `particular`, `masstransit`, `rebus` | `masstransit` |
| `--use-cases`  | Use case complexity | `basic`, `full`, `readonly`          | `basic`       |
