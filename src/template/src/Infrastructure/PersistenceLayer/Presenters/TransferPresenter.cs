using System.Collections.ObjectModel;
using Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;
using Genocs.CleanArchitecture.Template.Application.Interfaces;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.Presenters;

public sealed class TransferPresenter : IOutputPort<TransferOutput>
{
    public Collection<string> Errors { get; }
    public Collection<TransferOutput> Transfers { get; }

    public TransferPresenter()
    {
        Errors = [];
        Transfers = [];
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