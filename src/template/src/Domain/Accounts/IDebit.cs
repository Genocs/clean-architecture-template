using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Domain.Accounts;

public interface IDebit : IEntity
{
    PositiveMoney Sum(PositiveMoney amount);
}