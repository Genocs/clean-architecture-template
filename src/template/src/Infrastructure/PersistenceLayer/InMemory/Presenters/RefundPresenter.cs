using Genocs.CleanArchitecture.Template.Application.Boundaries.Refunds;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Presenters;

public sealed class RefundPresenter : IOutputPort
{
    public List<string> Errors { get; }
    public List<RefundOutput> Refunds { get; }

    public RefundPresenter()
    {
        Errors = new List<string>();
        Refunds = new List<RefundOutput>();
    }

    public void Error(string message)
    {
        Errors.Add(message);
    }

    public void Default(RefundOutput output)
    {
        Refunds.Add(output);
    }
}