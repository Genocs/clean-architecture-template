namespace Genocs.Domain.Customers
{
    using Genocs.Domain.Accounts;

    public interface ICustomer : IAggregateRoot
    {
        AccountCollection Accounts { get; }
        void Register(IAccount account);
    }
}