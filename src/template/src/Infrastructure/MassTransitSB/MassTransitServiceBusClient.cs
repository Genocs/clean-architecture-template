using Genocs.CleanArchitecture.Template.Application.Services;
using MassTransit;

namespace Genocs.CleanArchitecture.Template.Infrastructure.MassTransitSB;

public class MassTransitServiceBusClient(IPublishEndpoint publishEndpoint) : IServiceBusClient, IDisposable, IAsyncDisposable
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();

        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {

        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        await Task.CompletedTask;
    }

    public async Task PublishEventAsync<T>(T @event)
        where T : Contracts.Interfaces.IEvent
    {
        await _publishEndpoint.Publish(@event);

    }

    public async Task SendCommandAsync<T>(T command)
        where T : Contracts.Interfaces.ICommand
    {
        await _publishEndpoint.Publish(command);
    }
}
