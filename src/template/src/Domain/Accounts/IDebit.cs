namespace Genocs.MicroserviceLight.Template.Domain.Accounts
{
    using ValueObjects;

    public interface IDebit : IEntity
    {
        PositiveMoney Sum(PositiveMoney amount);
    }
}