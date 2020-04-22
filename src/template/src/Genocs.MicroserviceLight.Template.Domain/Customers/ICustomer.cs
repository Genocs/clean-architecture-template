namespace Genocs.MicroserviceLight.Template.Domain.Customers
{
    using Genocs.MicroserviceLight.Template.Domain.Accounts;

    public interface ICustomer : IAggregateRoot
    {
        AccountCollection Accounts { get; }
        void Register(IAccount account);
    }
}