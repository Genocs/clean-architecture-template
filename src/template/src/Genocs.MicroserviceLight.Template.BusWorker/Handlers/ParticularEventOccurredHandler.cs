using NServiceBus;
using NServiceBus.Logging;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusWorker.Handlers
{
    public class ParticularEventOccurredHandler : IHandleMessages<Shared.Events.RegistrationCompleted>
    {
        private readonly ILog _logger = LogManager.GetLogger<ParticularEventOccurredHandler>();

        //static int counter = 0;

        //public ParticularEventOccurredHandler(ILog logger)
        //    => _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));

        public Task Handle(Shared.Events.RegistrationCompleted message, IMessageHandlerContext context)
        {

            _logger.Info($"RegistrationCompleted on CustomerId: '{message.CustomerId}', AccountId: '{message.AccountId}', ");

            // Remove the comments to simulate some exception
            //if(counter++ < 10 )
            //{
            //    throw new InvalidOperationException($"exception number: '{counter}'");
            //}

            // Do something with the message here
            return Task.CompletedTask;
        }

    }
}
