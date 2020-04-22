namespace Genocs.MicroserviceLight.Template.UnitTests.InputValidationTests
{
    using Genocs.MicroserviceLight.Template.Application.Boundaries.Register;
    using Genocs.MicroserviceLight.Template.Application.Exceptions;
    using Genocs.MicroserviceLight.Template.Domain.ValueObjects;
    using Xunit;

    public sealed class RegisterInputValidationTests
    {
        [Fact]
        public void GivenNullSSN_InputNotCreated_ThrowsInputValidationException()
        {
            var actualEx = Assert.Throws<InputValidationException>(
                () => new RegisterInput(
                    null,
                    new Name("Giovanni"),
                    new PositiveMoney(10)
                ));
            Assert.Contains("ssn", actualEx.Message);
        }

        [Fact]
        public void GivenNullName_InputNotCreated_ThrowsInputValidationException()
        {
            var actualEx = Assert.Throws<InputValidationException>(
                () => new RegisterInput(
                    new SSN("19860817999"),
                    null,
                    new PositiveMoney(10)
                ));
            Assert.Contains("name", actualEx.Message);
        }

        [Fact]
        public void GivenNullPositiveAmount_InputNotCreated_ThrowsInputValidationException()
        {
            var actualEx = Assert.Throws<InputValidationException>(
                () => new RegisterInput(
                    new SSN("19860817999"),
                    new Name("Giovanni"),
                    null
                ));
            Assert.Contains("initialAmount", actualEx.Message);
        }

        [Fact]
        public void GivenValidData_InputCreated()
        {
            var actual = new RegisterInput(
                new SSN("19860817999"),
                new Name("Giovanni"),
                new PositiveMoney(10)
            );
            Assert.NotNull(actual);
        }
    }
}