using Genocs.CleanArchitecture.Template.Application.Boundaries.GetAccountDetails;
using Genocs.CleanArchitecture.Template.Application.Repositories;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class GetAccountDetails(IOutputPort outputHandler, IAccountRepository accountRepository) : IUseCase
{
    private readonly IOutputPort _outputHandler = outputHandler;
    private readonly IAccountRepository _accountRepository = accountRepository;

    public async Task ExecuteAsync(GetAccountDetailsInput input)
    {
        var account = await _accountRepository.Get(input.AccountId);

        if (account == null)
        {
            _outputHandler.NotFound($"The account {input.AccountId} does not exist or is not processed yet.");
            return;
        }

        GetAccountDetailsOutput output = new GetAccountDetailsOutput(account);
        _outputHandler.Default(output);
    }
}