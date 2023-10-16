namespace Genocs.CleanArchitecture.Template.Domain.Exceptions;

public sealed class MoneyShouldBePositiveException : DomainException
{
    internal MoneyShouldBePositiveException(string message)
        : base(message)
    {
    }
}