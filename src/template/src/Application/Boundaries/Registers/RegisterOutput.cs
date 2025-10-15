using Genocs.CleanArchitecture.Template.Domain.Accounts;
using Genocs.CleanArchitecture.Template.Domain.Customers;

namespace Genocs.CleanArchitecture.Template.Application.Boundaries.Registers;

public sealed class RegisterOutput
{
    public Customer Customer { get; }
    public Account Account { get; }

    public RegisterOutput(ICustomer customer, IAccount account)
    {
        var accountEntity = (Domain.Accounts.Account)account;

        List<Transaction> transactionResults = new List<Transaction>();
        foreach (var credit in accountEntity.Credits
                .GetTransactions())
        {
            Credit creditEntity = (Credit)credit;

            Transaction transactionOutput = new Transaction(
                Credit.Description,
                creditEntity
                .Amount
                .ToMoney()
                .ToDecimal(),
                creditEntity.TransactionDate);

            transactionResults.Add(transactionOutput);
        }

        foreach (var debit in accountEntity.Debits
                .GetTransactions())
        {
            Debit debitEntity = (Debit)debit;

            Transaction transactionOutput = new Transaction(
                Debit.Description,
                debitEntity
                .Amount
                .ToMoney()
                .ToDecimal(),
                debitEntity.TransactionDate);

            transactionResults.Add(transactionOutput);
        }

        Account = new Account(
            account.Id,
            account.GetCurrentBalance().ToDecimal(),
            transactionResults);

        List<Account> accountOutputs = new List<Account>();
        accountOutputs.Add(Account);

        Customer = new Customer(customer, accountOutputs);
    }
}