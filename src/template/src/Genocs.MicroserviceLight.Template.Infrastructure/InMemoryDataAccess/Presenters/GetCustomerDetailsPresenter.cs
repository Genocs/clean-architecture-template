namespace Genocs.MicroserviceLight.Template.Infrastructure.InMemoryDataAccess
{
    using Genocs.MicroserviceLight.Template.Application.Boundaries.GetCustomerDetails;
    using System.Collections.ObjectModel;

    public sealed class GetCustomerDetailsPresenter : IOutputPort
    {
        public Collection<string> Errors { get; }
        public Collection<GetCustomerDetailsOutput> GetCustomerDetails { get; }
        public Collection<string> NotFounds { get; }

        public GetCustomerDetailsPresenter()
        {
            Errors = new Collection<string>();
            GetCustomerDetails = new Collection<Application.Boundaries.GetCustomerDetails.GetCustomerDetailsOutput>();
            NotFounds = new Collection<string>();
        }

        public void Error(string message)
        {
            Errors.Add(message);
        }

        public void Default(Application.Boundaries.GetCustomerDetails.GetCustomerDetailsOutput output)
        {
            GetCustomerDetails.Add(output);
        }

        public void NotFound(string message)
        {
            NotFounds.Add(message);
        }
    }
}