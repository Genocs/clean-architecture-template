using NServiceBus;

namespace Genocs.MicroservicesLight.Template.ParticularShared.TransactionSaga
{
    /// <summary>
    /// This event happend when the system receive an Unload
    /// </summary>
    public class RedemptionCompleted : IEvent
    {
        public string RequestId { get; set; }
    }
}
