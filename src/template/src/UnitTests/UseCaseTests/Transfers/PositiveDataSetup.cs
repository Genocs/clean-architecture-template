using Xunit;

namespace Genocs.CleanArchitecture.Template.UnitTests.UseCaseTests.Transfers;

internal sealed class PositiveDataSetup : TheoryData<decimal, decimal>
{
    public PositiveDataSetup()
    {
        Add(100, 600);
        Add(200, 400);
    }
}