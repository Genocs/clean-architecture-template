using Genocs.CleanArchitecture.Template.Application.Boundaries.Registers;
using System.Collections.ObjectModel;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Presenters;

public sealed class RegisterPresenter : IOutputPort
{
    public Collection<string> Errors { get; }
    public Collection<RegisterOutput> Registers { get; }

    public RegisterPresenter()
    {
        Errors = new Collection<string>();
        Registers = new Collection<RegisterOutput>();
    }

    public void Error(string message)
    {
        Errors.Add(message);
    }

    public void Standard(RegisterOutput output)
    {
        Registers.Add(output);
    }
}