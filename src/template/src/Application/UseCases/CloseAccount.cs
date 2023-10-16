using Genocs.CleanArchitecture.Template.Application.Boundaries.CloseAccount;
using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class CloseAccount : IUseCase
{
    private readonly IOutputPort _outputHandler;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBusClient _serviceBus;
    private readonly IApiClient _orderApiClient;

    public CloseAccount(
        IOutputPort outputHandler,
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork,
        IServiceBusClient serviceBus,
        IApiClient orderApiClient)
    {
        _outputHandler = outputHandler;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
        _orderApiClient = orderApiClient;
    }

    public async Task Execute(CloseAccountInput closeAccountInput)
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
            await _serviceBus.PublishEventAsync(new Shared.Events.CloseAccountCompleted() { AccountId = account.Id });

            await _unitOfWork.Save();
        }

        var closeAccountOutput = new CloseAccountOutput(account);
        _outputHandler.Default(closeAccountOutput);
    }
}