using Genocs.CleanArchitecture.Template.Application.Boundaries.Withdraws;
using System.Collections.ObjectModel;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Presenters
{

    public sealed class WithdrawPresenter : IOutputPort
    {
        public Collection<string> Errors { get; }
        public Collection<WithdrawOutput> Withdrawals { get; }

        public WithdrawPresenter()
        {
            Errors = new Collection<string>();
            Withdrawals = new Collection<WithdrawOutput>();
        }

        public void Error(string message)
        {
            Errors.Add(message);
        }

        public void Default(WithdrawOutput output)
        {
            Withdrawals.Add(output);
        }
    }
}