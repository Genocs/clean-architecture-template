using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Genocs.Common.Domain.Entities;

namespace Genocs.CleanArchitecture.Template.Domain.Accounts;

public interface IDebit : IEntity<Guid>
{
    PositiveMoney Sum(PositiveMoney amount);
}