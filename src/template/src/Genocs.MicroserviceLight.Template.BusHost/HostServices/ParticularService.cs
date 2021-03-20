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
        private readonly Infrastructure.ServiceBus.ParticularServiceBusOptions _options;

        private readonly EndpointConfiguration _configuration;

        private IEndpointInstance _instance;


        public ParticularService(IOptions<Infrastructure.ServiceBus.ParticularServiceBusOptions> options, ILogger<ParticularService> logger)
        {
            _logger = logger;

            _options = options.Value;

            if (_options == null)
            {
                throw new NullReferenceException("options cannot be null");
            }

            // Start NServiceBus configuration
            _configuration = new EndpointConfiguration(_options.EndpointName);
            var transport = _configuration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString(_options.ConnectionString);

            // Unobtrusive mode. 
            var conventions = _configuration.Conventions();

            conventions.DefiningEventsAs(type => type.Namespace == "Genocs.MicroserviceLight.Template.Shared.Events");

            //conventions.DefiningEventsAs(type =>
            //    type.Namespace == "Genocs.MicroserviceLight.Template.Shared.Events"
            //    || typeof(IEvent).IsAssignableFrom(typeof(Shared.Events.EventOccurred))
            //);

            // https://docs.particular.net/nservicebus/serialization/
            _configuration.UseSerialization<NewtonsoftSerializer>();

            _configuration.EnableInstallers();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting...");
            _instance = await Endpoint.Start(_configuration);
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
