using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Deposits;

public sealed class DepositOutput
{
    public Transaction Transaction { get; }
    public decimal UpdatedBalance { get; }

    public DepositOutput(
        ICredit credit,
        Money updatedBalance)
    {
        Credit creditEntity = (Credit)credit;

        Transaction = new Transaction(
                                    creditEntity.Description,
                                    creditEntity
                                    .Amount
                                    .ToMoney()
                                    .ToDecimal(),
                                    creditEntity.TransactionDate);

        UpdatedBalance = updatedBalance.ToDecimal();
    }
}