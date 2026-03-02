namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Registers;

/// <summary>
/// The Output Port in hexagonal architecture (or Ports and Adapters).
/// It is a domain-defined interface, often called a driven or secondary port,
/// that abstracts external dependencies—such as databases, APIs, or message
/// queues—from the core business logic.
/// It allows the application to remain decoupled from technology.
/// </summary>
public interface IOutputPort : IErrorHandler
{
    void Standard(RegisterOutput registerOutput);
}