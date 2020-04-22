namespace Genocs.MicroserviceLight.Template.Application.Exceptions
{
    using Genocs.MicroserviceLight.Template.Domain;

    public sealed class InputValidationException : DomainException
    {
        public InputValidationException(string message) : base(message) { }
    }
}