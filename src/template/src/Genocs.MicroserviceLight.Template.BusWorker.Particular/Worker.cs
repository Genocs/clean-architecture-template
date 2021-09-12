namespace Genocs.MicroserviceLight.Template.ParticularBusWorker
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using NServiceBus;
    using ParticularBusWorker.Messages;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageSession _messageSession;

        public Worker(ILogger<Worker> logger, IMessageSession messageSession)
        {
            _logger = logger;
            _messageSession = messageSession;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                // Simple send the command
                await _messageSession
                        .Send(new DemoMessage { Payload = DateTimeOffset.Now.ToString() })
                        .ConfigureAwait(false);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
