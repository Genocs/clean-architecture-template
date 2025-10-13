using Genocs.CleanArchitecture.Template.Application.Boundaries.CloseAccount;
using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class CloseAccount(
                IOutputPort outputHandler,
                IAccountRepository accountRepository,
                IUnitOfWork unitOfWork,
                IServiceBusClient serviceBus,
                IApiClient orderApiClient) : IUseCase
{
    private readonly IOutputPort _outputHandler = outputHandler;
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IServiceBusClient _serviceBus = serviceBus;
    private readonly IApiClient _orderApiClient = orderApiClient;

    public async Task ExecuteAsync(CloseAccountInput closeAccountInput)
    {
        var account = await _accountRepository.Get(closeAccountInput.AccountId);
        if (account == null)
        {
            _outputHandler.Error($"The account '{closeAccountInput.AccountId}' does not exist or is already closed.");
            return;
        }

        if (account.IsClosingAllowed())
        {
            await _accountRepository.Delete(account);

            // Publish the event to the enterprise service bus
            await _serviceBus.PublishEventAsync(new Contracts.Events.CloseAccountCompleted() { AccountId = account.Id });

            await _unitOfWork.Save();
        }

        var closeAccountOutput = new CloseAccountOutput(account);
        _outputHandler.Default(closeAccountOutput);
    }
}