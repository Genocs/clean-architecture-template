namespace Genocs.MicroserviceLight.Template.Application.Services
{
    using Shared.ReadModels;
    using System.Threading.Tasks;

    public interface IDummyApiClient : IApiClient
    {
        Task<SimpleResult> GetSimpleModelAsync(string id);
    }
}
