namespace Genocs.MicroserviceLight.Template.BusHost.HostServices
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using NServiceBus;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ParticularService : IHostedService
    {

        private readonly ILogger<ParticularService> _logger;
        private readonly Infrastructure.ParticularServiceBus.ParticularServiceBusOptions _options;

        private readonly EndpointConfiguration _configuration;

        private IEndpointInstance _instance;


        public ParticularService(IOptions<Infrastructure.ParticularServiceBus.ParticularServiceBusOptions> options, ILogger<ParticularService> logger)
        {
            _options = options.Value;

            if (_options == null)
            {
                throw new NullReferenceException("options cannot be null");
            }

            _configuration = new EndpointConfiguration(_options.EndpointName);

            var transport = _configuration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString("host=localhost");
            transport.UseConventionalRoutingTopology();
            var routing = transport.Routing();

            _configuration.EnableInstallers();

            // Specify the routing for a specific type
            routing.RouteToEndpoint(typeof(Shared.Events.NServiceEventOccurred), "NServiceEventOccurred");

            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting...");

            _instance = await Endpoint.Start(_configuration);


            await _instance.Publish(new Shared.Events.NServiceEventOccurred { EventId = "sdfls" });

            _logger.LogInformation("Started");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping...");
            await _instance.Stop();
            _logger.LogInformation("Stopped");
        }
    }

}
