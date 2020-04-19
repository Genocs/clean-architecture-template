namespace Genocs.Domain.Accounts
{
    using Genocs.Domain.ValueObjects;

    public interface ICredit : IEntity
    {
        PositiveMoney Sum(PositiveMoney amount);
    }
}