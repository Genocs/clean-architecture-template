using NServiceBus;

namespace Genocs.CleanArchitecture.Template.Worker.Particular.Services;

public interface ICalculateStuff
{
    Task Calculate(int number);
}

internal class CalculateStuff : ICalculateStuff
{
    private readonly ILogger _logger;
    public IMessageSession? MessageSession { get; }

    public CalculateStuff(ILogger<CalculateStuff> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task Calculate(int number)
    {
        _logger.LogInformation($"Calculating the value for {number}");

        return Task.CompletedTask;
    }
}
