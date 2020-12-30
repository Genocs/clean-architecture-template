using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusHost.ExternalServices
{
    using Models;

    public interface ISimpleServiceCaller
    {
        Task<SimpleResult> GetSimpleModelAsync(string id);
    }
}
