using Genocs.CleanArchitecture.Template.Application.Boundaries.CloseAccount;
using System.Collections.ObjectModel;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Presenters;

public sealed class CloseAccountPresenter : IOutputPort
{
    public Collection<string> Errors { get; }
    public Collection<CloseAccountOutput> ClosedAccounts { get; }

    public CloseAccountPresenter()
    {
        Errors = new Collection<string>();
        ClosedAccounts = new Collection<CloseAccountOutput>();
    }

    public void Error(string message) => Errors.Add(message);

    public void Default(CloseAccountOutput output) => ClosedAccounts.Add(output);
}