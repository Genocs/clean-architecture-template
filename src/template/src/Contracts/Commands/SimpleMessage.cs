using Genocs.Common.CQRS.Commands;

namespace Genocs.CleanArchitecture.Template.Contracts.Commands;

public class SimpleMessage : ICommand
{
    public string? MessageId { get; set; }

    public string? MessageBody { get; set; }
}
