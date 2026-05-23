using Genocs.CleanArchitecture.Template.Application.Services;

namespace Genocs.CleanArchitecture.Template.UnitTests.TestFixtures;

public class FakeServiceBus : IServiceBusClient
{
    public async Task PublishEventAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : Genocs.Common.CQRS.Events.IEvent
    {
        await Task.CompletedTask;
    }

    public async Task SendCommandAsync<T>(T command, CancellationToken cancellationToken = default)
        where T : Genocs.Common.CQRS.Commands.ICommand
    {
        await Task.CompletedTask;
    }
}
