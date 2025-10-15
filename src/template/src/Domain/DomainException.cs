namespace Genocs.CleanArchitecture.Template.Domain;

public class DomainException(string businessMessage) : Exception(businessMessage);
