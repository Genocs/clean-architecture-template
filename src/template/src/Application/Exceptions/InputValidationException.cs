using Genocs.CleanArchitecture.Template.Domain;

namespace Genocs.CleanArchitecture.Template.Application.Exceptions;

public sealed class InputValidationException : DomainException
{
    public InputValidationException(string message)
        : base(message)
    {

    }
}