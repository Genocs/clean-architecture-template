namespace Genocs.MicroserviceLight.Template.Infrastructure.PersistenceLayer.InMemory
{
    using Application.Boundaries.Refund;
    using System.Collections.ObjectModel;

    public sealed class RefundPresenter : IOutputPort
    {
        public Collection<string> Errors { get; }
        public Collection<RefundOutput> Refunds { get; }

        public RefundPresenter()
        {
            Errors = new Collection<string>();
            Refunds = new Collection<RefundOutput>();
        }

        public void Error(string message)
        {
            Errors.Add(message);
        }

        public void Default(RefundOutput output)
        {
            Refunds.Add(output);
        }
    }
}