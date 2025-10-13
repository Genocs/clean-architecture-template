using Genocs.CleanArchitecture.Template.Application.Boundaries.Deposits;
using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class Deposit : IUseCase
{
    private readonly IEntityFactory _entityFactory;
    private readonly IOutputPort _outputHandler;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBusClient _serviceBus;

    public Deposit(
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

    public async Task ExecuteAsync(DepositInput input)
    {
        var account = await _accountRepository.Get(input.AccountId);
        if (account == null)
        {
            _outputHandler.Error($"The account {input.AccountId} does not exist or is already closed.");
            return;
        }

        var credit = account.Deposit(_entityFactory, input.Amount);

        await _accountRepository.Update(account, credit);

        // Publish the event to the enterprise service bus
        await _serviceBus.PublishEventAsync(new Shared.Events.DepositCompleted() { AccountId = input.AccountId, Amount = input.Amount.ToMoney().ToDecimal() });

        await _unitOfWork.Save();

        DepositOutput output = new DepositOutput(
            credit,
            account.GetCurrentBalance());

        _outputHandler.Default(output);
    }
}