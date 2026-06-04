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
[package-shield]: https://img.shields.io/badge/nuget-v.5.1.0-blue?&label=latest&logo=nuget
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

[![Hexagonal Architecture](https://raw.githubusercontent.com/Genocs/clean-architecture-template/main/assets/exagonal-architecture.png "Hexagonal Architecture")](https://github.com/Genocs/clean-architecture-template)

# Genocs Clean Architecture Template

A comprehensive .NET 10 project template that follows Clean Architecture principles and Domain-Driven Design (DDD). This template helps you rapidly scaffold microservices applications with built-in support for multiple databases, message brokers, and enterprise patterns.

## ✨ Features

- 🏗️ **Clean Architecture** - Domain, Application, Infrastructure, and Presentation layers
- 🎯 **Domain-Driven Design** - Rich domain models with proper separation of concerns
- 📨 **CQRS Pattern** - Command and event-driven workflows via `Genocs.Common.CQRS`
- 🚌 **Message Brokers** - MassTransit, NServiceBus, and Rebus options with `RabbitMQ`
- 🗃️ **Multiple Databases** - MongoDB, EF Core (SQL Server), and InMemory options
- 🔍 **Telemetry and Logging** - Built-in telemetry/logging via `Genocs.Logging` and `Genocs.Telemetry` integration with monitoring stack assets
- 🐳 **Containerization** - Docker and Kubernetes ready
- ☁️ **Infrastructure as Code** - Bicep, Terraform, Helm, and Kubernetes manifests included
- ⚡ **Background Services** - Worker services for async processing
- 🧪 **Comprehensive Testing** - Unit, Integration, and Acceptance tests
- 📘 **API Versioning and OpenAPI** - Versioned endpoints and OpenAPI `Genocs.WebApi.OpenApi` support out of the box

## 📋 Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Quick Start](#quick-start)
- [Template Options](#template-options)
- [Architecture Overview](#architecture-overview)
- [Development Workflow](#development-workflow)
- [Troubleshooting](#troubleshooting)
- [Community & Support](#community--support)
- [Contributing](#contributing)
- [License](#license)

## 📋 Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- **IDE** (choose one):
  - [Visual Studio](https://visualstudio.microsoft.com/vs/) with .NET 10 support
  - [Visual Studio Code](https://code.visualstudio.com/) with C# tooling
  - [JetBrains Rider](https://www.jetbrains.com/rider/)
- **Optional for local infrastructure** (depends on selected template options):
  - [Docker Desktop](https://www.docker.com/products/docker-desktop)
  - MongoDB for `--database mongodb`
  - SQL Server for `--database sqlserver` (default)
  - RabbitMQ for `--service-bus masstransit|rebus|nservicebus`
  - Azure Service Bus namespace for `--service-bus azureservicebus`
  - [Reqnroll](https://docs.reqnroll.net/latest/installation/setup-ide.html) plugin for Testing

## 🚀 Quick Start

### Install the Template

```bash
# Install the latest version
dotnet new install Genocs.CleanArchitecture.Template

# Or install a specific version
dotnet new install Genocs.CleanArchitecture.Template::5.1.0

# View all available options
dotnet new gnx-cleanarchitecture --help

# Create a project using template defaults
dotnet new gnx-cleanarchitecture --name "CompanyName.ServiceName"

# Create a project with explicit options
dotnet new gnx-cleanarchitecture \
  --name "CompanyName.ServiceName" \
  --database inmemory \
  --service-bus nservicebus \
  --use-cases basic
```

## 🏗️ Architecture Overview

The template generates a solution with the following structure:

```pl
src/
├── AcceptanceTests/ # Acceptance Tests
├── Application/ # Use cases and application services
├── Contracts/ # API and message contracts (commands, events, messages)
├── Contracts.NServiceBus/ # Included when --service-bus nservicebus is selected
├── Domain/ # Core business logic and entities
├── Infrastructure/ # Data access and external services
├── IntegrationTests/ # Integration Tests
├── UnitTests/ # Unit Tests
├── WebApi/ # REST API controllers and middleware
├── Worker/ # Background services and message handlers
└── ...
```

### Key Components

- **Domain Layer**: Entities, value objects, domain services
- **Application Layer**: CQRS handlers, interfaces, DTOs
- **Infrastructure Layer**: Repositories, message brokers, databases
- **Presentation Layer**: Controllers, middleware, API documentation

## How to Build and Install the Template Locally

To build the package run the following commands:

```bash
# To clone the repository
git clone https://github.com/Genocs/clean-architecture-template
cd clean-architecture-template

# To pack and install the template
dotnet pack ./src/Package.Template.csproj -p:PackageVersion=5.1.0 --configuration Release --output ./out

dotnet new install ./out/Genocs.CleanArchitecture.Template.5.1.0.nupkg

# To verify the installation and see available options
dotnet new gnx-cleanarchitecture --help

# To uninstall the template
dotnet new uninstall Genocs.CleanArchitecture.Template

# Example of creating a new project with InMemory database and Masstransit as service bus
dotnet new gnx-cleanarchitecture --name CompanyName.ServiceName --database inmemory --service-bus masstransit
```

Official Documentation:
- [custom-templates](https://learn.microsoft.com/en-us/dotnet/core/tools/custom-templates)
- [dotnet-templating](https://github.com/dotnet/templating)


### Miscellaneous

Useful commands:

```bash
# How to get the list of installed templates
dotnet new -u

# How to get the list of templates
dotnet new list
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

Please check the [documentation](https://learn.microsoft.com/en-us/visualstudio/ide/how-to-locate-and-organize-project-and-item-templates?view=visualstudio) for more details.

## Contributing

Contributions are welcome. See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines and development workflow.

## Changelog

View complete [Changelog](https://github.com/Genocs/clean-architecture-template/blob/main/CHANGELOG.md).

## License

This project is licensed with the [MIT license](LICENSE).

## Code Contributors

This project exists thanks to all the people who contribute. [Submit your PR and join the team!](CONTRIBUTING.md)

[![genocs contributors](https://contrib.rocks/image?repo=Genocs/clean-architecture-template "genocs contributors")](https://github.com/Genocs/clean-architecture-template/graphs/contributors)

## Financial Contributors

Become a financial contributor and help me sustain the project.

**Support the Project** on [Opencollective](https://opencollective.com/genocs)

[![Opencollective](https://opencollective.com/genocs/individuals.svg?width=890 "Opencollective")](https://opencollective.com/genocs)

## ⚙️ Template Options

| Option         | Description         | Values                               | Default       |
| -------------- | ------------------- | ------------------------------------ | ------------- |
| `--name`       | Project name        | `{Company.Project.Service}`          | Required      |
| `--database`   | Database provider   | `mongodb`, `sqlserver`, `inmemory`   | `sqlserver`     |
| `--service-bus`| Message broker      | `azureservicebus`, `masstransit`, `nservicebus`, `rebus` | `rebus` |
| `--use-cases`  | Use case complexity | `full`, `basic`, `readonly`          | `full`        |
