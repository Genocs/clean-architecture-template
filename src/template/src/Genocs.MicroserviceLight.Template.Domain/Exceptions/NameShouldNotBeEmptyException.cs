namespace Genocs.MicroserviceLight.Template.Domain.Exceptions
{
    public sealed class NameShouldNotBeEmptyException : DomainException
    {
        internal NameShouldNotBeEmptyException(string message) : base(message) { }
    }
}