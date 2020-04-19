namespace Genocs.UnitTests.InputValidationTests
{
    using Genocs.Application.Boundaries.GetAccountDetails;
    using Genocs.Application.Exceptions;
    using System;
    using Xunit;

    public sealed class GetAccountDetailsInputValidationTests
    {
        [Fact]
        public void GivenEmptyAccountId_InputNotCreated_ThrowsInputValidationException()
        {
            var actualEx = Assert.Throws<InputValidationException>(
                () => new GetAccountDetailsInput(
                    Guid.Empty
                ));
            Assert.Contains("accountId", actualEx.Message);
        }

        [Fact]
        public void GivenValidData_InputCreated()
        {
            var actual = new GetAccountDetailsInput(
                Guid.NewGuid()
            );
            Assert.NotNull(actual);
        }
    }
}