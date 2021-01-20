using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.Infrastructure.WebApiClient.ExternalServices
{
    using Shared.ReadModels;

    public interface ISimpleAuthServiceCaller
    {
        Task<SimpleResult> GetSimpleAuthModelAsync(string id);
    }
}
