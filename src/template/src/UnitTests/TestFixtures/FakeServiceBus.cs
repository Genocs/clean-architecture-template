using Genocs.CleanArchitecture.Template.Application.Services;

namespace Genocs.CleanArchitecture.Template.UnitTests.TestFixtures;

public class FakeServiceBus : IServiceBusClient
{
    public async Task PublishEventAsync<T>(T evt)
        where T : Shared.Interfaces.IEvent
    {
        await Task.CompletedTask;
    }

    public async Task SendCommandAsync<T>(T cmd)
        where T : Shared.Interfaces.ICommand
    {
        await Task.CompletedTask;
    }
}
