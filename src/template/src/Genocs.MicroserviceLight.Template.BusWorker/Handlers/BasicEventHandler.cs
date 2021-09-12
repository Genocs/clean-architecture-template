using NServiceBus;
using NServiceBus.Logging;
using Shared;
using System.Threading.Tasks;

namespace WorkerService.Handlers
{
    /// <summary>
    /// Basic event handler.
    /// This event handler will be registerd automatcally. It do not need any action to be up and running 
    /// </summary>
    public class BasicEventHandler : IHandleMessages<MyEvent>
    {
        static ILog _log = LogManager.GetLogger<BasicEventHandler>();

        public Task Handle(MyEvent message, IMessageHandlerContext context)
        {
            _log.Info($"WorkerService.BasicEventHandler has received a message");
            return Task.CompletedTask;
        }
    }
}
