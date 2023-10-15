using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Shared.Interfaces;

namespace Genocs.CleanArchitecture.Template.UnitTests.TestFixtures;

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
