using Genocs.CleanArchitecture.Template.ContractsNServiceBus.Commands;

namespace Genocs.CleanArchitecture.Template.Worker.ParticularSB;

public class BackgroundWorker(ILogger<BackgroundWorker> logger, IMessageSession messageSession) : BackgroundService
{
    private readonly ILogger<BackgroundWorker> _logger = logger;
    private readonly IMessageSession _messageSession = messageSession;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            // Simple send the command
            await _messageSession
                    .Send(new TimeTriggreredCommand { Payload = "Hello from trigger" }, cancellationToken: stoppingToken)
                    .ConfigureAwait(false);

            await Task.Delay(1000, stoppingToken);
        }
    }
}
