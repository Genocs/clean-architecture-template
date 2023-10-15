using NServiceBus;

namespace Genocs.MicroserviceLight.Template.ParticularShared.TransactionSaga
{
    /// <summary>
    /// This event happen when the system receive an Unload
    /// </summary>
    public class RedemptionRejected : IEvent
    {
        public string RequestId { get; set; }
        public string TransactionId { get; set; }
    }
}
