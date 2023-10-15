using Genocs.CleanArchitecture.Template.Shared.Interfaces;

namespace Genocs.CleanArchitecture.Template.Shared.Commands;

public class SimpleMessage : ICommand
{
    public string? MessageId { get; set; }

    public string? MessageBody { get; set; }

}
