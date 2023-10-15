namespace Genocs.MicroserviceLight.Template.Infrastructure.ServiceBus
{
    using Microsoft.Extensions.Options;
    using Rebus.Activation;
    using Rebus.Config;
    using System;
    using System.Threading.Tasks;

    using Application.Services;

    public class RebusServiceBusClient : IServiceBusClient, IDisposable, IAsyncDisposable
    {
        private BuiltinHandlerActivator _activator;
        public RebusServiceBusClient(IOptions<RebusBusSettings> settings)
        {
            RebusBusSettings optionsInstance = settings?.Value;

            _activator = new BuiltinHandlerActivator();

            Configure.With(_activator)
                .Logging(l => l.ColoredConsole(Rebus.Logging.LogLevel.Info))
                .Transport(t => t.UseRabbitMqAsOneWayClient(optionsInstance.TransportConnection))
               .Start();
        }

        public async Task SendCommandAsync<T>(T cmd) where T : Shared.Interfaces.ICommand
        {
            // Check the ContextId Management
            await _activator.Bus.Send(cmd);
        }

        public async Task PublishEventAsync<T>(T evt) where T : Shared.Interfaces.IEvent
        {
            await _activator.Bus.Publish(evt);
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
            if (disposing)
            {
                _activator?.Dispose();
            }
            _activator = null;
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            //if (_bus is not null)
            //{
            //    await _bus.DisposeAsync();
            //}

            //_bus = null;

            await Task.CompletedTask;
        }
    }
}
