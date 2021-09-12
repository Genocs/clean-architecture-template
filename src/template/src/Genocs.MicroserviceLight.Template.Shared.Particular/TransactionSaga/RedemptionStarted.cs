using NServiceBus;

namespace Genocs.MicroservicesLight.Template.ParticularShared.TransactionSaga
{
    /// <summary>
    /// This event happen when the system start processing the redemption
    /// </summary>
    public class RedemptionStarted : IEvent
    {
        public string RequestId { get; set; }
    }
}
