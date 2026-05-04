using Xunit;

namespace Genocs.CleanArchitecture.Template.UnitTests.UseCaseTests.Deposits;

internal sealed class NegativeDataSetup : TheoryData<decimal>
{
    public NegativeDataSetup()
    {
        Add(-100);
    }
}