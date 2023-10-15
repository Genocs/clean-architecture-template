using Genocs.CleanArchitecture.Template.Application.Boundaries.Registers;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Register;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Xunit;

namespace Genocs.CleanArchitecture.Template.UnitTests.PresenterTests;


public sealed class RegisterPresenterTests
{
    [Fact]
    public void GivenValidData_Handle_WritesOkObjectResult()
    {
        var customer = new Infrastructure.PersistenceLayer.InMemory.Customer(
            new SSN("198608178888"),
            new Name("Nocco Giovanni Emanuele")
        );

        var account = new Infrastructure.PersistenceLayer.InMemory.Account(
            customer
        );

        var registerOutput = new RegisterOutput(
            customer,
            account
        );

        var sut = new RegisterPresenter();
        sut.Standard(registerOutput);

        var actual = Assert.IsType<CreatedAtRouteResult>(sut.ViewModel);
        Assert.Equal((int)HttpStatusCode.Created, actual.StatusCode);

        var actualValue = (RegisterResponse)actual.Value;
        Assert.Equal(customer.Id, actualValue.CustomerId);
    }
}