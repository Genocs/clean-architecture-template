using Genocs.CleanArchitecture.Template.Application.Exceptions;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Registers;

public sealed class RegisterInput
{
    public SSN SSN { get; }
    public Name Name { get; }
    public PositiveMoney InitialAmount { get; }

    public RegisterInput(SSN ssn, Name name, PositiveMoney initialAmount)
    {
        SSN = ssn ?? throw new InputValidationException($"{nameof(ssn)} cannot be null.");
        Name = name ?? throw new InputValidationException($"{nameof(name)} cannot be null.");
        InitialAmount = initialAmount ?? throw new InputValidationException($"{nameof(initialAmount)} cannot be null.");
    }
}