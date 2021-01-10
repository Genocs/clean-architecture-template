using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusHost.HostServices
{
    using Handlers;
    using Microsoft.Extensions.Logging;
    using Rebus.Activation;
    using Rebus.Bus;
    using Rebus.Config;
    using Shared.Events;
    using System;

    internal class RebusService : IHostedService
    {
        private readonly JsonSerializer _serializer;
        private readonly ILogger<RebusService> _logger;
        private readonly Infrastructure.RebusServiceBus.RebusBusOptions _options;


        private BuiltinHandlerActivator _activator;
        private IBus _bus;


        public RebusService(IOptions<Infrastructure.RebusServiceBus.RebusBusOptions> options, ILogger<RebusService> logger)
        {
            _options = options.Value;

            if (_options == null)
            {
                throw new NullReferenceException("options cannot be null");
            }

            _logger = logger;

            _serializer = new JsonSerializer();

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting...");

            _activator = new BuiltinHandlerActivator();

            _activator.Register(() => new Handler());

            _bus = Configure.With(_activator)
                .Logging(l => l.ColoredConsole(minLevel: Rebus.Logging.LogLevel.Debug))
                .Transport(t => t.UseRabbitMq(_options.TransportConnection, _options.QueueName))
                .Options(o => o.SetMaxParallelism(1))
                .Start();


            _activator.Bus.Subscribe<EventOccurred>().Wait();

            _logger.LogInformation("Started");
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping...");
            await Task.CompletedTask;
            _logger.LogInformation("Stopped");
        }
    }
}
