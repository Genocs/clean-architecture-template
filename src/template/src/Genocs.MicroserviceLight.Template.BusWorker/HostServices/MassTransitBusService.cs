namespace Genocs.MicroserviceLight.Template.BusWorker.HostServices
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class MassTransitBusService : IHostedService
    {
        private readonly ILogger<MassTransitBusService> _logger;
        private readonly Infrastructure.ServiceBus.MassTransitSetting _settings;

        private readonly IServiceProvider _serviceProvider;


        public MassTransitBusService(IOptions<Infrastructure.ServiceBus.MassTransitSetting> options, ILogger<MassTransitBusService> logger, IServiceProvider serviceProvider)
        {
            _settings = options.Value;

            if (_settings == null)
            {
                throw new NullReferenceException("options cannot be null");
            }

            _serviceProvider = serviceProvider;

            _logger = logger;
        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting...");
            await Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping...");
            _logger.LogInformation("Stopped");
            await Task.CompletedTask;
        }
    }
}
