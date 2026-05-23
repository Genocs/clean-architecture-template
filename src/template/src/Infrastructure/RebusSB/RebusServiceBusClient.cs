using Genocs.CleanArchitecture.Template.Application.Services;
using Microsoft.Extensions.Options;
using Rebus.Activation;
using Rebus.Config;
using Rebus.Logging;

namespace Genocs.CleanArchitecture.Template.Infrastructure.RebusSB;

public class RebusServiceBusClient : IServiceBusClient, IDisposable, IAsyncDisposable
{
    private BuiltinHandlerActivator _activator;

    private bool _disposed;

    public RebusServiceBusClient(IOptions<RebusBusSettings> settings)
    {
        var optionsInstance = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

        _activator = new BuiltinHandlerActivator();

        Configure.With(_activator)
            .Logging(l => l.ColoredConsole(LogLevel.Info))
            .Transport(t => t.UseRabbitMqAsOneWayClient(optionsInstance.TransportConnection))
           .Start();
    }

    public async Task SendCommandAsync<T>(T command, CancellationToken cancellationToken = default)
        where T : Genocs.Common.CQRS.Commands.ICommand
    {
        // Check the ContextId Management
        await _activator.Bus.Send(command);
    }

    public async Task PublishEventAsync<T>(T message, CancellationToken cancellationToken = default)
        where T : Genocs.Common.CQRS.Events.IEvent
    {
        await _activator.Bus.Publish(message);
    }

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
        if (!_disposed)
        {
            _disposed = true;
            if (disposing)
            {
                _activator.Dispose();
            }
        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        /*
        if (_bus is not null)
        {
            await _bus.DisposeAsync();
        }

        _bus = null;
        */

        await Task.CompletedTask;
    }
}
