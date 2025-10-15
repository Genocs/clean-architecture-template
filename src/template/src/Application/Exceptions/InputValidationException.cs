using Genocs.CleanArchitecture.Template.Domain;

namespace Genocs.CleanArchitecture.Template.Application.Exceptions;

public sealed class InputValidationException(string message) : DomainException(message);
