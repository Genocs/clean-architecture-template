using Genocs.CleanArchitecture.Template.Application.Boundaries.GetCustomerDetails;
using Genocs.CleanArchitecture.Template.Application.Repositories;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class GetCustomerDetails : IUseCase
{
    private readonly IOutputPort _outputHandler;
    private readonly ICustomerRepository _customerRepository;
    private readonly IAccountRepository _accountRepository;

    public GetCustomerDetails(
                              IOutputPort outputHandler,
                              ICustomerRepository customerRepository,
                              IAccountRepository accountRepository)
    {
        _outputHandler = outputHandler;
        _customerRepository = customerRepository;
        _accountRepository = accountRepository;
    }

    public async Task ExecuteAsync(GetCustomerDetailsInput input)
    {
        var customer = await _customerRepository.Get(input.CustomerId);

        if (customer == null)
        {
            _outputHandler.NotFound($"The customer {input.CustomerId} does not exist or is not processed yet.");
            return;
        }

        List<Account> accounts = new List<Account>();

        foreach (var accountId in customer.Accounts.GetAccountIds())
        {
            var account = await _accountRepository.Get(accountId);

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