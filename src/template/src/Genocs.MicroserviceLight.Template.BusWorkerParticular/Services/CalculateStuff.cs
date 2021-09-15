namespace Genocs.MicroserviceLight.Template.BusWorkerParticular.Services
{
    using Microsoft.Extensions.Logging;
    using NServiceBus;
    using System.Threading.Tasks;

    public interface ICalculateStuff
    {
        Task Calculate(int number);
    }

    internal class CalculateStuff : ICalculateStuff
    {
        private readonly ILogger logger;

        public CalculateStuff(ILogger<CalculateStuff> logger)
        {
            this.logger = logger;
        }

        public IMessageSession MessageSession { get; }

        public Task Calculate(int number)
        {
            logger.LogInformation($"Calculating the value for {number}");

            return Task.CompletedTask;
        }
    }
}
