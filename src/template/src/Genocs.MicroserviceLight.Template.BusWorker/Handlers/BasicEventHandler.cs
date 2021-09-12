namespace Genocs.MicroserviceLight.Template.BusWorker.Handlers
{
    using Shared.Particular.IntegrationEvents;
    using NServiceBus;
    using NServiceBus.Logging;
    using System.Threading.Tasks;
    using ParticularShared.TransactionSaga;
    using System;


    /// <summary>
    /// Basic event handler.
    /// This event handler will be registerd automatcally. It do not need any action to be up and running 
    /// </summary>
    public class BasicEventHandler : IHandleMessages<BasicEvent>
    {
        static ILog _log = LogManager.GetLogger<BasicEventHandler>();

        public async Task Handle(BasicEvent message, IMessageHandlerContext context)
        {
            _log.Info($"WorkerService.BasicEventHandler has received a message");


            await context.SendLocal(new TransactionLoaded()
            {
                RequestId = Guid.NewGuid().ToString(),
                TransactionId = Guid.NewGuid().ToString()
            });
        }
    }
}
