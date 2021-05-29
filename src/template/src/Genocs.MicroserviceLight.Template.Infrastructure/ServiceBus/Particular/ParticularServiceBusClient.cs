namespace Genocs.MicroserviceLight.Template.Infrastructure.ServiceBus
{
    using Application.Services;
    using Microsoft.Extensions.Options;
    using NServiceBus;
    using System;
    using System.Threading.Tasks;

    public class ParticularServiceBusClient : IServiceBusClient, IDisposable, IAsyncDisposable
    {

        private readonly ParticularServiceBusSettings _settings;
        private IEndpointInstance _instance;

        public ParticularServiceBusClient(IOptions<ParticularServiceBusSettings> settings)
        {
            _settings = settings.Value;

            if (_settings is null)
            {
                throw new NullReferenceException("settings.Value.cannot be null");
            }
        }

        private async Task Initialize()
        {
            if (_instance == null)
            {
                var endpointConfiguration = new EndpointConfiguration(_settings.EndpointName);
                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                transport.UseConventionalRoutingTopology();
                transport.ConnectionString(_settings.ConnectionString);

                // Unobtrusive mode. 
                var conventions = endpointConfiguration.Conventions();

                conventions.DefiningEventsAs(type => type.Namespace == "Genocs.MicroserviceLight.Template.Shared.Events");

                //conventions.DefiningEventsAs(type =>
                //    type.Namespace == "Genocs.MicroserviceLight.Template.Shared.Events"
                //    || typeof(IEvent).IsAssignableFrom(typeof(Shared.Events.EventOccurred))
                //);

                // https://docs.particular.net/nservicebus/serialization/
                endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
                endpointConfiguration.EnableInstallers();

                _instance = await Endpoint.Start(endpointConfiguration);
            }
        }


        public async Task PublishEventAsync<T>(T evt) where T : Shared.Interfaces.IEvent
        {
            await Initialize();
            await _instance.Publish(evt);
        }

        public async Task SendCommandAsync<T>(T cmd) where T : Shared.Interfaces.ICommand
        {
            await Initialize();
            await _instance.Send(cmd);
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
                //_instance.Stop();
            }
        }

        protected virtual async ValueTask DisposeAsyncCore()
        {
            await _instance.Stop();
            await Task.CompletedTask;
        }
    }
}