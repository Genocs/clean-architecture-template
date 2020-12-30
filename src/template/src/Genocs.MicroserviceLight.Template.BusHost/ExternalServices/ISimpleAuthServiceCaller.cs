using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusHost.ExternalServices
{
    using Models;

    public interface ISimpleAuthServiceCaller
    {
        Task<SimpleResult> GetSimpleAuthModelAsync(string id);
    }
}
