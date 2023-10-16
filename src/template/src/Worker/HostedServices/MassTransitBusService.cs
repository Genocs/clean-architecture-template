using Genocs.CleanArchitecture.Template.Infrastructure.ServiceBus.MassTransit;
using Microsoft.Extensions.Options;

namespace Genocs.CleanArchitecture.Template.Worker.HostedServices;

internal class MassTransitBusService : IHostedService
{
    private readonly ILogger<MassTransitBusService> _logger;
    private readonly MassTransitSetting _settings;

    private readonly IServiceProvider _serviceProvider;


    public MassTransitBusService(IOptions<MassTransitSetting> options, ILogger<MassTransitBusService> logger, IServiceProvider serviceProvider)
    {
        _settings = options.Value;

        if (_settings == null)
        {
            throw new NullReferenceException("options cannot be null");
        }

        _serviceProvider = serviceProvider;

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
