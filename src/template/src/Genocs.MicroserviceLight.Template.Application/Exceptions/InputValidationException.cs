namespace Genocs.MicroserviceLight.Template.Application.Exceptions
{
    using Domain;

    public sealed class InputValidationException : DomainException
    {
        public InputValidationException(string message) : base(message) { }
    }
}