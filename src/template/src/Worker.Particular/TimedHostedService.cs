using Genocs.CleanArchitecture.Template.Worker.Particular.Messages;
using NServiceBus;

namespace Genocs.CleanArchitecture.Template.Worker.Particular;

public class TimedHostedService : IHostedService, IDisposable
{
    private int _executionCount = 0;
    private readonly ILogger<TimedHostedService> _logger;
    private Timer? _timer;

    public IMessageSession MessageSession { get; }

    public TimedHostedService(ILogger<TimedHostedService> logger, IMessageSession messageSession)
    {
        _logger = logger;
        MessageSession = messageSession;
    }

    public async Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(5));

        await MessageSession.Send(new DemoMessage());
    }

    private void DoWork(object state)
    {
        var count = Interlocked.Increment(ref _executionCount);



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
        _timer?.Dispose();
    }
}
