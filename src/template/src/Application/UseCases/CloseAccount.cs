using Genocs.CleanArchitecture.Template.Application.Boundaries.CloseAccount;
using Genocs.CleanArchitecture.Template.Application.Interfaces;
using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class CloseAccount(
                IOutputPort<CloseAccountOutput> outputHandler,
                IAccountRepository accountRepository,
                IUnitOfWork unitOfWork,
                IServiceBusClient serviceBus) : IUseCase<CloseAccountInput>
{
    private readonly IOutputPort<CloseAccountOutput> _outputHandler = outputHandler ?? throw new ArgumentNullException(nameof(outputHandler));
    private readonly IAccountRepository _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IServiceBusClient _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));

    public async Task ExecuteAsync(CloseAccountInput closeAccountInput, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetAsync(closeAccountInput.AccountId, cancellationToken);
        if (account == null)
        {
            _outputHandler.Error($"The account '{closeAccountInput.AccountId}' does not exist or is already closed.");
            return;
        }

        if (account.IsClosingAllowed())
        {
            await _accountRepository.DeleteAsync(account, cancellationToken);

            await _serviceBus.PublishEventAsync(new Contracts.Events.CloseAccountCompleted() { AccountId = account.Id }, cancellationToken);

            await _unitOfWork.SaveAsync(cancellationToken);
        }

        var closeAccountOutput = new CloseAccountOutput(account);
        _outputHandler.Default(closeAccountOutput);
    }
}