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
[downloads-prev-shield]: https://img.shields.io/nuget/dt/Genocs.CleanArchitectureTemplate.svg?color=2da44e&label=downloads%20prev&logo=nuget
[downloads-prev-url]: https://www.nuget.org/packages/Genocs.CleanArchitectureTemplate
[package-shield]: https://img.shields.io/badge/nuget-v.4.0.0-blue?&label=latests&logo=nuget
[package-url]: https://github.com/Genocs/clean-architecture-template/actions/workflows/build_and_test.yml
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

[![logo](https://raw.githubusercontent.com/Genocs/clean-architecture-template/main/assets/genocs-library-logo.png "logo")](https://github.com/Genocs/clean-architecture-template)


# Clean Architecture Template 
Built for .NET9. It incorporates the most essential Packages your projects will ever need. Follows Clean Architecture Principles.

The template can be used with the `dotnet new` command or with the `Visual Studio 2022` or `Visual Studio Code` IDEs.

## Goals

This is an Application Template built to help you create LOB applications. It follows the Clean Architecture Principles and built on Domain-Driven-Design. This tool is useful to increases productivity on developing your next microservices.
The template allows you to use different databases and enterprise service bus.

### Databases supported:
- InMemoryDB (for development purpose)
- SqlServer
- MongoDB


### Enterprise Service Bus libraries supported:
- Particular NServiceBus
- MassTransit
- Rebus

## Prerequisites
- [.NET 8.x](https://dotnet.microsoft.com/download/dotnet/8.0)

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/preview/vs2022/)(optional)
- [Visual Studio Code](https://code.visualstudio.com/download)(optional)
- [Rider](https://www.jetbrains.com/rider/)(optional)


## Getting Started

Open up your *Command Prompt* or *PowerShell* or *bash* and run the following command.

``` bash
# To clone the repository
git clone https://github.com/Genocs/clean-architecture-template
# To build the nuget package
nuget pack ./src/Package.Template.nuspec -NoDefaultExcludes -OutputDirectory ./out -Version 4.0.0
```


``` bash
# To install the template
cd ./out
dotnet new install Genocs.CleanArchitecture.Template
```

or, 

if you want to use a specific version of the template, 

use

``` bash
cd ./out
dotnet new install Genocs.CleanArchitecture.Template::4.0.0
```

This would install the `Genocs CleanArchitecture Template` globally on your machine.

For more details on getting started, [read the documentation](https://genocs-blog.netlify.app/library/)

## Changelogs

View Complete [Changelogs](https://github.com/Genocs/clean-architecture-template/blob/main/CHANGELOGS.md).

## License

This project is licensed with the [MIT license](LICENSE).


## Community

- Discord [@genocs](https://discord.com/invite/fWwArnkV)
- Facebook Page [@genocs](https://facebook.com/Genocs)
- Youtube Channel [@genocs](https://youtube.com/c/genocs)

## Support

Has this Project helped you learn something New? or Helped you at work?

Here are a few ways by which you can support.

- ⭐ Leave a star!
- 🥇 Recommend this project to your colleagues.
- 🦸 Do consider endorsing me on LinkedIn for ASP.NET Core - [Connect via LinkedIn](https://www.linkedin.com/in/giovanni-emanuele-nocco-b31a5169/) 
- ☕ If you want to support this project in the long run, consider [buying me a coffee](https://www.buymeacoffee.com/genocs)!
  

[![buy-me-a-coffee](https://raw.githubusercontent.com/Genocs/clean-architecture-template/main/assets/buy-me-a-coffee.png "buy me a coffee")](https://www.buymeacoffee.com/genocs)


## Financial Contributors

Become a financial contributor and help me sustain the project.

**Support the Project** on [Opencollective](https://opencollective.com/genocs).
