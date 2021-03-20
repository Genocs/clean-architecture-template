namespace Genocs.MicroserviceLight.Template.Infrastructure.PersistenceLayer.InMemory
{
    using Application.Boundaries.GetCustomerDetails;
    using System.Collections.ObjectModel;

    public sealed class GetCustomerDetailsPresenter : IOutputPort
    {
        public Collection<string> Errors { get; }
        public Collection<GetCustomerDetailsOutput> GetCustomerDetails { get; }
        public Collection<string> NotFounds { get; }

        public GetCustomerDetailsPresenter()
        {
            Errors = new Collection<string>();
            GetCustomerDetails = new Collection<GetCustomerDetailsOutput>();
            NotFounds = new Collection<string>();
        }

        public void Error(string message)
        {
            Errors.Add(message);
        }

        public void Default(GetCustomerDetailsOutput output)
        {
            GetCustomerDetails.Add(output);
        }

        public void NotFound(string message)
        {
            NotFounds.Add(message);
        }
    }
}