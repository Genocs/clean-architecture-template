using Xunit;

namespace Genocs.CleanArchitecture.Template.UnitTests.UseCaseTests.Registers;

internal sealed class PositiveDataSetup : TheoryData<decimal>
{
    public PositiveDataSetup()
    {
        Add(0);
        Add(100);
        Add(200);
    }
}