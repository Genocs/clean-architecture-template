using Genocs.CleanArchitecture.Template.Application.Services;

namespace Genocs.CleanArchitecture.Template.UnitTests.TestFixtures;

public class FakeServiceBus : IServiceBusClient
{
    public async Task PublishEventAsync<T>(T evt)
        where T : Genocs.Common.CQRS.Events.IEvent
    {
        await Task.CompletedTask;
    }

    public async Task SendCommandAsync<T>(T cmd)
        where T : Genocs.Common.CQRS.Commands.ICommand
    {
        await Task.CompletedTask;
    }
}
