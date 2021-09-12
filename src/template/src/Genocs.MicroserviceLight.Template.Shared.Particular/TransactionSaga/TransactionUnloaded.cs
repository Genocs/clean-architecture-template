using NServiceBus;

namespace Genocs.MicroservicesLight.Template.ParticularShared.TransactionSaga
{
    /// <summary>
    /// This event happen whenever the system receive an Unload
    /// </summary>
    public class TransactionUnloaded : IEvent
    {
        public string RequestId { get; set; }
    }
}
