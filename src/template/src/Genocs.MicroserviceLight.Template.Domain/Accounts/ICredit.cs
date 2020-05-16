namespace Genocs.MicroserviceLight.Template.Domain.Accounts
{
    using ValueObjects;

    public interface ICredit : IEntity
    {
        PositiveMoney Sum(PositiveMoney amount);
    }
}