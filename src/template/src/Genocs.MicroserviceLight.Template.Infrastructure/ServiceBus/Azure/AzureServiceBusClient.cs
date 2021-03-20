namespace Genocs.MicroserviceLight.Template.Infrastructure.ServiceBus
{
    using Application.Services;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Shared.Interfaces;
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;
    using System.Threading.Tasks;

    public class AzureServiceBusClient : IServiceBusClient, IDisposable, IAsyncDisposable
    {

        private readonly AzureServiceBusOptions _config;

        private IQueueClient _queueClient;

        public AzureServiceBusClient(IOptions<AzureServiceBusOptions> configuration)
        {
            _config = configuration.Value;

            if (_config is null)
            {
                throw new NullReferenceException("configuration.Value.cannot be null");
            }

            ServiceBusConnectionStringBuilder connectionStringBuilder = new ServiceBusConnectionStringBuilder
            {
                Endpoint = _config.QueueEndpoint,
                EntityPath = _config.QueueName,
                SasKeyName = _config.QueueAccessPolicyName,
                SasKey = _config.QueueAccessPolicyKey,
                TransportType = TransportType.Amqp
            };

            _queueClient = new QueueClient(connectionStringBuilder)
            {
                PrefetchCount = _config.PrefetchCount
            };
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

            }
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

        public async Task PublishEventAsync<T>(T @event) where T : IEvent
        {
            Message msg = new Message();
            string strMsg = JsonConvert.SerializeObject(@event);

            /*
             * Please evaluate how ho implement a more efficient way
             * to serialize the event to a byte
             * 
             * */
            //BinaryFormatter bf = new BinaryFormatter();
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    bf.Serialize(ms, evt);
            //    msg.Body = ms.ToArray();
            //}

            msg.Body = Encoding.ASCII.GetBytes(strMsg);
            await _queueClient.SendAsync(msg);
        }

        public async Task SendCommandAsync<T>(T command) where T : ICommand
        {
            Message msg = new Message();
            string strMsg = JsonConvert.SerializeObject(command);

            /*
             * Please evaluate how ho implement a more efficient way
             * to serialize the event to a byte
             * 
             * */
            //BinaryFormatter bf = new BinaryFormatter();
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    bf.Serialize(ms, evt);
            //    msg.Body = ms.ToArray();
            //}

            msg.Body = Encoding.ASCII.GetBytes(strMsg);
            await _queueClient.SendAsync(msg);
        }
    }
}
