namespace Genocs.CleanArchitecture.Template.Domain.Exceptions;

public sealed class SSNShouldNotBeEmptyException : DomainException
{
    internal SSNShouldNotBeEmptyException(string message)
        : base(message)
    {
    }
}