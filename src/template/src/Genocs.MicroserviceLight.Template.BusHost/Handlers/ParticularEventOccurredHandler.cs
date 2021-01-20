using NServiceBus;
using NServiceBus.Logging;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusHost.Handlers
{
    public class ParticularEventOccurredHandler : IHandleMessages<Shared.Events.RegistrationCompleted>
    {
        static ILog _logger = LogManager.GetLogger<ParticularEventOccurredHandler>();

        public Task Handle(Shared.Events.RegistrationCompleted message, IMessageHandlerContext context)
        {

            _logger.Info($"RegistrationCompleted on AccountId: '{message.AccountId}'");

            // Do something with the message here
            return Task.CompletedTask;
        }
    }

}
