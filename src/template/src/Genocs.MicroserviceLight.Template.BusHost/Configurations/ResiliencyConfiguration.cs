namespace Genocs.MicroserviceLight.Template.BusHost.Configurations
{
    public class ResiliencyConfiguration
    {
        public int MaxRetries { get; set; }

        public double CircuitBreakerThreshold { get; set; }

        public int CircuitBreakerSamplingPeriodSeconds { get; set; }

        public int CircuitBreakerMinimumThroughput { get; set; }

        public int CircuitBreakerBreakDurationSeconds { get; set; }

        public int MaxBulkheadSize { get; set; }

        public int MaxBulkheadQueueSize { get; set; }
    }
}
