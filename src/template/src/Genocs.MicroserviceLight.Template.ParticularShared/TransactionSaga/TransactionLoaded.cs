using NServiceBus;

namespace Genocs.MicroservicesLight.Template.ParticularShared.TransactionSaga
{
    /// <summary>
    /// This event happen whenever the system receive a Load
    /// </summary>
    public class TransactionLoaded : IEvent
    {
        public string RequestId { get; set; }
    }
}
