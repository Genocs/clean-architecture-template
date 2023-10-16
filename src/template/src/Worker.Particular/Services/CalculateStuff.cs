using Microsoft.Extensions.Logging;
using NServiceBus;
using System.Threading.Tasks;

namespace Genocs.CleanArchitecture.Template.Worker.Particular.Services
{
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
