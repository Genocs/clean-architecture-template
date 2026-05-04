using Genocs.CleanArchitecture.Template.Application.Boundaries.GetCustomerDetails;
using Genocs.CleanArchitecture.Template.Application.Repositories;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class GetCustomerDetails(
                          IOutputPort outputHandler,
                          ICustomerRepository customerRepository,
                          IAccountRepository accountRepository) : IUseCase
{
    private readonly IOutputPort _outputHandler = outputHandler;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task ExecuteAsync(GetCustomerDetailsInput input, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetAsync(input.CustomerId, cancellationToken);

        if (customer == null)
        {
            _outputHandler.NotFound($"The customer {input.CustomerId} does not exist or is not processed yet.");
            return;
        }

        List<Account> accounts = new List<Account>();

        foreach (var accountId in customer.Accounts.GetAccountIds())
        {
            var account = await _accountRepository.GetAsync(accountId);

            if (account != null)
            {
                Account accountOutput = new Account(account);
                accounts.Add(accountOutput);
            }
        }

        GetCustomerDetailsOutput output = new GetCustomerDetailsOutput(customer, accounts);
        _outputHandler.Default(output);
    }
}