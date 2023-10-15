using Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;
using System.Collections.ObjectModel;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Presenters;

public sealed class TransferPresenter : IOutputPort
{
    public Collection<string> Errors { get; }
    public Collection<TransferOutput> Transfers { get; }

    public TransferPresenter()
    {
        Errors = new Collection<string>();
        Transfers = new Collection<TransferOutput>();
    }

    public void Error(string message)
    {
        Errors.Add(message);
    }

    public void Default(TransferOutput output)
    {
        Transfers.Add(output);
    }
}