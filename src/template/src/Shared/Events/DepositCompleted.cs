using System;

namespace Genocs.MicroserviceLight.Template.Shared.Events
{
    public class DepositCompleted : Interfaces.IEvent
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
