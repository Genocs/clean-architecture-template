namespace Genocs.CleanArchitecture.Template.Domain.Accounts
{
    using Genocs.CleanArchitecture.Template.Domain;
    using ValueObjects;

    public interface IDebit : IEntity
    {
        PositiveMoney Sum(PositiveMoney amount);
    }
}