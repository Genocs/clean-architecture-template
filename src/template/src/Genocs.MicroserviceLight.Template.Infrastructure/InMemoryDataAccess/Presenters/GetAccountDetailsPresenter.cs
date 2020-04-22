namespace Genocs.MicroserviceLight.Template.Infrastructure.InMemoryDataAccess
{
    using System.Collections.ObjectModel;
    using Genocs.MicroserviceLight.Template.Application.Boundaries.GetAccountDetails;

    public sealed class GetAccountDetailsPresenter : IOutputPort
    {
        public Collection<string> Errors { get; }
        public Collection<GetAccountDetailsOutput> GetAccountDetails { get; }
        public Collection<string> NotFounds { get; }

        public GetAccountDetailsPresenter()
        {
            Errors = new Collection<string>();
            GetAccountDetails = new Collection<GetAccountDetailsOutput>();
            NotFounds = new Collection<string>();
        }

        public void Error(string message)
        {
            Errors.Add(message);
        }

        public void Default(Application.Boundaries.GetAccountDetails.GetAccountDetailsOutput output)
        {
            GetAccountDetails.Add(output);
        }

        public void NotFound(string message)
        {
            NotFounds.Add(message);
        }
    }
}