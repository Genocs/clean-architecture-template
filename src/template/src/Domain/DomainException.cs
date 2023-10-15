namespace Genocs.CleanArchitecture.Template.Domain
{
    using System;

    public class DomainException : Exception
    {
        public DomainException(string businessMessage) : base(businessMessage) { }
    }
}