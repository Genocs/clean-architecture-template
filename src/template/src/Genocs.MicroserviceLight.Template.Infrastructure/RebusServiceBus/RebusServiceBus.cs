using Genocs.MicroserviceLight.Template.Application.Services;
using Genocs.MicroserviceLight.Template.Shared;
using Microsoft.Extensions.Options;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Serialization.Json;
using System;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.Infrastructure.RebusServiceBus
{
    public class RebusServiceBus : IServiceBus, IDisposable, IAsyncDisposable
    {
        private BuiltinHandlerActivator _activator;
        private IBus _bus;

        const string InputQueueName = "another.queue";
        const string InputTopicName = "some-topic";

        public RebusServiceBus(IOptions<RebusBusOptions> configuration)
        {
            RebusBusOptions optionsInstance = configuration?.Value;

            _activator = new BuiltinHandlerActivator();

            //_activator.Register(() => new PrintDateTime());

            _bus = Configure.With(_activator)
                .Logging(l => l.ColoredConsole(Rebus.Logging.LogLevel.Info))
                .Transport(t => t.UseRabbitMqAsOneWayClient(optionsInstance.TransportConnection))
                .Serialization(s => s.UseNewtonsoftJson(JsonInteroperabilityMode.PureJson))
                .Start();
        }

        public async Task SendCommandAsync<T>(T cmd) where T : Shared.Interfaces.ICommand
        {
            await _bus.Send(new Job() { JobNumber = 12 });
        }

        public async Task PublishEventAsync<T>(T evt) where T : Shared.Interfaces.IEvent
        {
            await _bus.Advanced.Topics.Publish(InputTopicName, new Job() { JobNumber = 12 });
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
                _bus?.Dispose();
                _activator?.Dispose();
            }
            _bus = null;
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
