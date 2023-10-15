namespace Genocs.CleanArchitecture.Template.Domain.Customers
{
    using Accounts;
    using Genocs.CleanArchitecture.Template.Domain;
    using Genocs.CleanArchitecture.Template.Domain.Accounts;

    public interface ICustomer : IAggregateRoot
    {
        AccountCollection Accounts { get; }
        void Register(IAccount account);
    }
}