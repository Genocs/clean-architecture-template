using NServiceBus;

namespace Genocs.CleanArchitecture.Template.Worker.ParticularSB.ExternalServices;

public interface ICalculateStuff
{
    Task Calculate(int number);
}

internal class CalculateStuff(ILogger<CalculateStuff> logger) : ICalculateStuff
{
    private readonly ILogger _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    public IMessageSession? MessageSession { get; }

    public Task Calculate(int number)
    {
        _logger.LogInformation($"Calculating the value for {number}");

        return Task.CompletedTask;
    }
}
