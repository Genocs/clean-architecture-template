namespace Genocs.Domain.Accounts
{
    using Genocs.Domain.ValueObjects;

    public interface IDebit : IEntity
    {
        PositiveMoney Sum(PositiveMoney amount);
    }
}