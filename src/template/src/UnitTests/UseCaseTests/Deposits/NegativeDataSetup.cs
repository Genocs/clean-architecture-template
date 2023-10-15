namespace Genocs.CleanArchitecture.Template.UnitTests.UseCaseTests.Deposits
{
    using Xunit;

    internal sealed class NegativeDataSetup : TheoryData<decimal>
    {
        public NegativeDataSetup()
        {
            Add(-100);
        }
    }
}