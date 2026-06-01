namespace Genocs.CleanArchitecture.Template.Worker.MassTransitSB.HostedServices;

/// <summary>
/// This is a background service that can be used to perform any initialization or cleanup tasks or just to run scheduled tasks.
/// It is registered as a hosted service in the dependency injection container and will be started and stopped automatically by the host.
/// </summary>
internal class BackgroundHostedService : IHostedService
{
    private readonly ILogger<BackgroundHostedService> _logger;

    public BackgroundHostedService(ILogger<BackgroundHostedService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting...");
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping...");
        _logger.LogInformation("Stopped");
        await Task.CompletedTask;
    }
}
