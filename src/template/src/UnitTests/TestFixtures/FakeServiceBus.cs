namespace Genocs.MicroserviceLight.Template.UnitTests.TestFixtures
{
    using Application.Services;
    using Shared.Interfaces;
    using System.Threading.Tasks;

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
