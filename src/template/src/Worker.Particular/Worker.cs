using Genocs.CleanArchitecture.Template.Worker.Particular.Messages;
using NServiceBus;

namespace Genocs.CleanArchitecture.Template.Worker.Particular;

public class Worker(ILogger<Worker> logger, IMessageSession messageSession) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private readonly IMessageSession _messageSession = messageSession;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            // Simple send the command
            await _messageSession
                    .Send(new DemoMessage { Payload = DateTimeOffset.Now.ToString() }, cancellationToken: stoppingToken)
                    .ConfigureAwait(false);

            await Task.Delay(1000, stoppingToken);
        }
    }
}
