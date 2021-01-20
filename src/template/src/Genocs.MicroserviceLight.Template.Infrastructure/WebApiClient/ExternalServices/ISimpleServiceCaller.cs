namespace Genocs.MicroserviceLight.Template.Infrastructure.WebApiClient.ExternalServices
{
    using Shared.ReadModels;
    using System.Threading.Tasks;

    public interface ISimpleServiceCaller
    {
        Task<SimpleResult> GetSimpleModelAsync(string id);
    }
}
