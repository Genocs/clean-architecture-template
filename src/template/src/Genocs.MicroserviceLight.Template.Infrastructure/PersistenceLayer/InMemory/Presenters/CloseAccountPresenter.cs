namespace Genocs.MicroserviceLight.Template.Infrastructure.PersistenceLayer.InMemory
{
    using Application.Boundaries.CloseAccount;
    using System.Collections.ObjectModel;

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
}