namespace Genocs.MicroserviceLight.Template.Application.Services
{
    using Shared.ReadModels;
    using System.Threading.Tasks;

    public interface IAuthApiClient : IApiClient
    {
        Task<SimpleResult> GetSimpleAuthModelAsync(string id);
    }
}
