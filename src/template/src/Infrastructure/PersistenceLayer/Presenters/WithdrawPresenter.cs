using System.Collections.ObjectModel;
using Genocs.CleanArchitecture.Template.Application.Boundaries.Withdraws;
using Genocs.CleanArchitecture.Template.Application.Interfaces;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.Presenters;

public sealed class WithdrawPresenter : IOutputPort<WithdrawOutput>
{
    public Collection<string> Errors { get; }
    public Collection<WithdrawOutput> Withdrawals { get; }

    public WithdrawPresenter()
    {
        Errors = [];
        Withdrawals = [];
    }

    public void Error(string message)
        => Errors.Add(message);

    public void Default(WithdrawOutput output)
        => Withdrawals.Add(output);
}