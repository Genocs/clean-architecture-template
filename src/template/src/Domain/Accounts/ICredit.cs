namespace Genocs.CleanArchitecture.Template.Domain.Accounts
{
    using Genocs.CleanArchitecture.Template.Domain;
    using ValueObjects;

    public interface ICredit : IEntity
    {
        PositiveMoney Sum(PositiveMoney amount);
    }
}