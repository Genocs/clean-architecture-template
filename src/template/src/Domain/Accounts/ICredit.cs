using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Domain.Accounts;

public interface ICredit : IEntity
{
    PositiveMoney Sum(PositiveMoney amount);
}