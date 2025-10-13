using Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;
using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class Transfer(
            IEntityFactory entityFactory,
            IOutputPort outputHandler,
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork,
            IServiceBusClient serviceBus) : IUseCase
{
    private readonly IEntityFactory _entityFactory = entityFactory;
    private readonly IOutputPort _outputHandler = outputHandler;
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IServiceBusClient _serviceBus = serviceBus;

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
        await _serviceBus.PublishEventAsync(new Contracts.Events.TransferCompleted() { OriginalAccountId = originAccount.Id, DestinationAccountId = destinationAccount.Id, Amount = input.Amount.ToMoney().ToDecimal() });

        await _unitOfWork.Save();

        TransferOutput output = new TransferOutput(
                                                   debit,
                                                   originAccount.GetCurrentBalance(),
                                                   input.OriginAccountId,
                                                   input.DestinationAccountId);

        _outputHandler.Default(output);
    }
}