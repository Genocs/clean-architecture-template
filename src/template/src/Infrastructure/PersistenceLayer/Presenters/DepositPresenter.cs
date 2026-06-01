using System.Collections.ObjectModel;
using Genocs.CleanArchitecture.Template.Application.Boundaries.Deposits;
using Genocs.CleanArchitecture.Template.Application.Interfaces;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.Presenters;

public sealed class DepositPresenter : IOutputPort<DepositOutput>
{
    public Collection<string> Errors { get; }
    public Collection<DepositOutput> Deposits { get; }

    public DepositPresenter()
    {
        Errors = [];
        Deposits = [];
    }

    public void Error(string message)
        => Errors.Add(message);

    public void Default(DepositOutput output)
        => Deposits.Add(output);
}