using Genocs.CleanArchitecture.Template.Application.Boundaries.GetAccountDetails;
using Genocs.CleanArchitecture.Template.Application.Interfaces;
using Genocs.CleanArchitecture.Template.Application.Repositories;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class GetAccountDetails(IOutputPort outputHandler, IAccountRepository accountRepository) : IUseCase<GetAccountDetailsInput>
{
    private readonly IOutputPort _outputHandler = outputHandler ?? throw new ArgumentNullException(nameof(outputHandler));
    private readonly IAccountRepository _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));

    public async Task ExecuteAsync(GetAccountDetailsInput input, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetAsync(input.AccountId, cancellationToken);

        if (account == null)
        {
            _outputHandler.NotFound($"The account {input.AccountId} does not exist or is not processed yet.");
            return;
        }

        GetAccountDetailsOutput output = new GetAccountDetailsOutput(account);
        _outputHandler.Default(output);
    }
}