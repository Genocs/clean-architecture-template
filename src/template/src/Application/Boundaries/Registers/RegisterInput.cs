using Genocs.CleanArchitecture.Template.Application.Exceptions;
using Genocs.CleanArchitecture.Template.Application.Interfaces;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Registers;

public sealed class RegisterInput(SSN? ssn, Name? name, PositiveMoney? initialAmount) : IInputType
{
    public SSN SSN { get; } = ssn ?? throw new InputValidationException($"{nameof(ssn)} cannot be null.");
    public Name Name { get; } = name ?? throw new InputValidationException($"{nameof(name)} cannot be null.");
    public PositiveMoney InitialAmount { get; } = initialAmount ?? throw new InputValidationException($"{nameof(initialAmount)} cannot be null.");
}