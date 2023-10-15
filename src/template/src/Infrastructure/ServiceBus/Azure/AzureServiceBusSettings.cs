namespace Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.Azure
{
    public class AzureServiceBusSettings
    {
        public AzureServiceBusSettings()
        {
            MaxConcurrency = 20;
            PrefetchCount = 3000;
        }

        public string QueueEndpoint { get; set; }

        public string QueueName { get; set; }

        public string QueueAccessPolicyName { get; set; }

        public string QueueAccessPolicyKey { get; set; }

        public int MaxConcurrency { get; internal set; }

        public int PrefetchCount { get; internal set; }
    }
}
