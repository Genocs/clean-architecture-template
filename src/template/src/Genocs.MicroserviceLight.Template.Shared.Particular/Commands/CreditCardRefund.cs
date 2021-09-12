using NServiceBus;

namespace Genocs.MicroservicesLight.Template.ParticularShared.Commands
{


    public class FeeUnloaded : ICommand
    {
        public string TransactionId { get; set; }
    }

    public class UnloadCompleted : ICommand
    {
        public string TransactionId { get; set; }
    }

    public class RefundCompleted : ICommand
    {
        public string TransactionId { get; set; }
    }
}
