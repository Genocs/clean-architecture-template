using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusWorker.HostServices
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
        private readonly ILogger<RebusService> _logger;
        private readonly Infrastructure.ServiceBus.RebusBusOptions _options;

        private BuiltinHandlerActivator _activator;
        private IBus _bus;

        public RebusService(IOptions<Infrastructure.ServiceBus.RebusBusOptions> options, ILogger<RebusService> logger)
        {
            _logger = logger;

            _options = options.Value;

            if (_options == null)
            {
                throw new NullReferenceException("options cannot be null");
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting...");

            // Start rebus configuration
            _activator = new BuiltinHandlerActivator();

            _activator.Register((r) => new RebusEventOccurredHandler());

            _bus = Configure.With(_activator)
                .Logging(l => l.ColoredConsole(minLevel: Rebus.Logging.LogLevel.Debug))
                .Transport(t => t.UseRabbitMq(_options.TransportConnection, _options.QueueName))
                .Options(o => o.SetMaxParallelism(1))
                .Start();


            // Subscribe the event
            await _activator.Bus.Subscribe<RegistrationCompleted>();

            _logger.LogInformation("Started");

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping...");
            _logger.LogInformation("Stopped");
            await Task.CompletedTask;
        }
    }
}
