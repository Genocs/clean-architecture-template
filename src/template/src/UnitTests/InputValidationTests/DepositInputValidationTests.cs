using Genocs.CleanArchitecture.Template.Application.Boundaries.Deposits;
using Genocs.CleanArchitecture.Template.Application.Exceptions;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Xunit;

namespace Genocs.CleanArchitecture.Template.UnitTests.InputValidationTests;


public sealed class DepositInputValidationTests
{
    [Fact]
    public void GivenEmptyAccountId_InputNotCreated_ThrowsInputValidationException()
    {
        var actualEx = Assert.Throws<InputValidationException>(
            () => new DepositInput(
                Guid.Empty,
                new PositiveMoney(10)
            ));
        Assert.Contains("accountId", actualEx.Message);
    }

    [Fact]
    public void GivenNullAmount_InputNotCreated_ThrowsInputValidationException()
    {
        var actualEx = Assert.Throws<InputValidationException>(
            () => new DepositInput(
                Guid.NewGuid(),
                null
            ));
        Assert.Contains("amount", actualEx.Message);
    }

    [Fact]
    public void GivenValidData_InputCreated()
    {
        var actual = new DepositInput(
            Guid.NewGuid(),
            new PositiveMoney(10)
        );
        Assert.NotNull(actual);
    }
}