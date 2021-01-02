using Genocs.MicroserviceLight.Template.Shared.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusHost.RequestProcessing
{
    public interface IRequestProcessor
    {
        Task<bool> ProcessSimpleMessageAsync(SimpleMessage message, IReadOnlyDictionary<string, object> properties);
    }
}
