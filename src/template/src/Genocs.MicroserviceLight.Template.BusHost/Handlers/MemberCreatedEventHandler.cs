using NServiceBus;
using NServiceBus.Logging;
using System.Threading.Tasks;
using UTU.TaxFree.DTO.Member.Bus.Events;

namespace Genocs.MicroserviceLight.Template.BusHost.Handlers
{
    public class MemberCreatedEventHandler : IHandleMessages<MemberCreated>
    {
        static ILog _logger = LogManager.GetLogger<ParticularEventHandler>();

        public Task Handle(MemberCreated message, IMessageHandlerContext context)
        {

            _logger.Info($"MemberCreatedEventHandler. Received message with MemberId: '{message.MemberId}'");

            // Do something with the message here
            return Task.CompletedTask;
        }
    }

}
