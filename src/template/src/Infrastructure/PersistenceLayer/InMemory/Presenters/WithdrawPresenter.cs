namespace Genocs.MicroserviceLight.Template.Infrastructure.PersistenceLayer.InMemory
{
    using Application.Boundaries.Withdraw;
    using System.Collections.ObjectModel;

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