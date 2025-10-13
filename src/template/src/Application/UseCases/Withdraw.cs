using Genocs.CleanArchitecture.Template.Application.Boundaries.Withdraws;
using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class Withdraw : IUseCase
{
    private readonly IEntityFactory _entityFactory;
    private readonly IOutputPort _outputHandler;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBusClient _serviceBus;

    public Withdraw(
        IEntityFactory entityFactory,
        IOutputPort outputHandler,
        IAccountRepository accountRepository,
        IUnitOfWork unitOfWork,
        IServiceBusClient serviceBus)
    {
        _entityFactory = entityFactory;
        _outputHandler = outputHandler;
        _accountRepository = accountRepository;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }

    public async Task ExecuteAsync(WithdrawInput input)
    {
        var account = await _accountRepository.Get(input.AccountId);
        if (account == null)
        {
            _outputHandler.Error($"The account {input.AccountId} does not exist or is already closed.");
            return;
        }

        var debit = account.Withdraw(_entityFactory, input.Amount);

        if (debit == null)
        {
            _outputHandler.Error($"The account {input.AccountId} does not have enough funds to withdraw {input.Amount}.");
            return;
        }

        await _accountRepository.Update(account, debit);

        // Publish the event to the enterprise service bus
        await _serviceBus.PublishEventAsync(new Shared.Events.WithdrawCompleted() { AccountId = input.AccountId, Amount = input.Amount.ToMoney().ToDecimal() });

        await _unitOfWork.Save();

        WithdrawOutput output = new WithdrawOutput(
            debit,
            account.GetCurrentBalance());

        _outputHandler.Default(output);
    }
}