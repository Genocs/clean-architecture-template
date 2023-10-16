namespace Genocs.CleanArchitecture.Template.Domain.Exceptions;

internal sealed class InvalidSSNException : DomainException
{
    internal InvalidSSNException(string message)
        : base(message)
    {
    }
}