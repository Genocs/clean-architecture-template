using Genocs.CleanArchitecture.Template.ContractsNServiceBus.Commands;

namespace Genocs.CleanArchitecture.Template.Worker.HostedServices;

public class TimedHostedService(ILogger<TimedHostedService> logger, IMessageSession messageSession) : IHostedService, IDisposable
{
    private readonly ILogger<TimedHostedService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    private int _executionCount = 0;
    private Timer? _timer;

    public IMessageSession MessageSession { get; } = messageSession ?? throw new ArgumentNullException(nameof(messageSession));

    public async Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(
                            DoWork,
                            null,
                            TimeSpan.Zero,
                            TimeSpan.FromSeconds(5));

        await MessageSession.Send(new TimeTriggreredCommand() { Payload = "Hello form TimedHostedService!" }, cancellationToken: stoppingToken);
    }

    private void DoWork(object? state)
    {
        int count = Interlocked.Increment(ref _executionCount);

        _logger.LogInformation(
            "Timed Hosted Service is working. Count: {Count}", count);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _timer?.Dispose();
    }
}
