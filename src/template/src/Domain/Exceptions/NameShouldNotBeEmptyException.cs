using Genocs.CleanArchitecture.Template.Domain;

namespace Genocs.CleanArchitecture.Template.Domain.Exceptions
{
    public sealed class NameShouldNotBeEmptyException : DomainException
    {
        internal NameShouldNotBeEmptyException(string message) : base(message) { }
    }
}