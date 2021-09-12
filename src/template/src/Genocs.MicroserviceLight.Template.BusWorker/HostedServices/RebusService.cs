namespace Genocs.MicroserviceLight.Template.BusWorker.HostedServices
{
    using Handlers;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Rebus.Activation;
    using Rebus.Bus;
    using Rebus.Config;
    using Shared.Events;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class RebusService : IHostedService
    {
        private readonly ILogger<RebusService> _logger;
        private readonly Infrastructure.ServiceBus.RebusBusSettings _settings;

        private BuiltinHandlerActivator _activator;
        private IBus _bus;

        public RebusService(IOptions<Infrastructure.ServiceBus.RebusBusSettings> settings, ILogger<RebusService> logger)
        {
            _logger = logger;

            _settings = settings.Value;

            if (_settings == null)
            {
                throw new NullReferenceException("options cannot be null");
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting...");

            // Start rebus configuration
            _activator = new BuiltinHandlerActivator();

            _activator.Register((r) => new RebusEventOccurredHandler(_logger));

            _bus = Configure.With(_activator)
                .Logging(l => l.ColoredConsole(minLevel: Rebus.Logging.LogLevel.Debug))
                .Transport(t => t.UseRabbitMq(_settings.TransportConnection, _settings.QueueName))
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
