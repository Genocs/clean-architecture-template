namespace Genocs.MicroserviceLight.Template.Infrastructure.InMemoryDataAccess
{
    using Application.Boundaries.Deposit;
    using System.Collections.ObjectModel;

    public sealed class DepositPresenter : IOutputPort
    {
        public Collection<string> Errors { get; }
        public Collection<DepositOutput> Deposits { get; }

        public DepositPresenter()
        {
            Errors = new Collection<string>();
            Deposits = new Collection<DepositOutput>();
        }

        public void Error(string message)
        {
            Errors.Add(message);
        }

        public void Default(DepositOutput output)
        {
            Deposits.Add(output);
        }
    }
}