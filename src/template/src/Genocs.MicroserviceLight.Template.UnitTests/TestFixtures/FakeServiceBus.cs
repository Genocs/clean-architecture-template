using Genocs.MicroserviceLight.Template.Application.Services;
using Genocs.MicroserviceLight.Template.Shared.Interfaces;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.UnitTests.TestFixtures
{
    public class FakeServiceBus : IServiceBusClient
    {
        public async Task PublishEventAsync<T>(T evt) where T : IEvent
        {
            await Task.CompletedTask;
        }

        public async Task SendCommandAsync<T>(T cmd) where T : ICommand
        {
            await Task.CompletedTask;
        }
    }
}
