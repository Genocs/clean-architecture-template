using Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;
using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class Transfer : IUseCase
{
    private readonly IEntityFactory _entityFactory;
    private readonly IOutputPort _outputHandler;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBusClient _serviceBus;

    public Transfer(
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

    public async Task ExecuteAsync(TransferInput input)
    {
        var originAccount = await _accountRepository.Get(input.OriginAccountId);
        if (originAccount == null)
        {
            _outputHandler.Error($"The account {input.OriginAccountId} does not exist or is already closed.");
            return;
        }

        var destinationAccount = await _accountRepository.Get(input.DestinationAccountId);
        if (destinationAccount == null)
        {
            _outputHandler.Error($"The account {input.DestinationAccountId} does not exist or is already closed.");
            return;
        }

        var debit = originAccount.Withdraw(_entityFactory, input.Amount);
        var credit = destinationAccount.Deposit(_entityFactory, input.Amount);

        await _accountRepository.Update(originAccount, debit);
        await _accountRepository.Update(destinationAccount, credit);

        // Publish the event to the enterprise service bus
        await _serviceBus.PublishEventAsync(new Shared.Events.TransferCompleted() { OriginalAccountId = originAccount.Id, DestinationAccountId = destinationAccount.Id, Amount = input.Amount.ToMoney().ToDecimal() });

        await _unitOfWork.Save();

        TransferOutput output = new TransferOutput(
                                                   debit,
                                                   originAccount.GetCurrentBalance(),
                                                   input.OriginAccountId,
                                                   input.DestinationAccountId);

        _outputHandler.Default(output);
    }
}