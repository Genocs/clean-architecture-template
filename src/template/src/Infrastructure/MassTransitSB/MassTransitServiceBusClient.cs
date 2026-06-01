using Genocs.CleanArchitecture.Template.Application.Services;
using MassTransit;

namespace Genocs.CleanArchitecture.Template.Infrastructure.MassTransitSB;

public class MassTransitServiceBusClient(IPublishEndpoint publishEndpoint) : IServiceBusClient, IDisposable, IAsyncDisposable
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));

    public async Task PublishEventAsync<T>(T message, CancellationToken cancellationToken = default)
    where T : Genocs.Common.CQRS.Events.IEvent
    {
        await _publishEndpoint.Publish(message, cancellationToken);

    }

    public async Task SendCommandAsync<T>(T command, CancellationToken cancellationToken = default)
        where T : Genocs.Common.CQRS.Commands.ICommand
    {
        await _publishEndpoint.Publish(command, cancellationToken);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeCoreAsync();

        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {

        }
    }

    protected virtual async ValueTask DisposeCoreAsync()
    {
        await Task.CompletedTask;
    }
}
