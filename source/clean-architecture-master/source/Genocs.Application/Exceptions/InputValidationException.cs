namespace Genocs.Application.Exceptions
{
    using Genocs.Domain;

    public sealed class InputValidationException : DomainException
    {
        public InputValidationException(string message) : base(message) { }
    }
}