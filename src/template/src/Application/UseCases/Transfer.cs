using Genocs.CleanArchitecture.Template.Application.Boundaries.Transfers;
using Genocs.CleanArchitecture.Template.Application.Interfaces;
using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class Transfer(
            IEntityFactory entityFactory,
            IOutputPort<TransferOutput> outputHandler,
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork,
            IServiceBusClient serviceBus) : IUseCase<TransferInput>
{
    private readonly IEntityFactory _entityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory));
    private readonly IOutputPort<TransferOutput> _outputHandler = outputHandler ?? throw new ArgumentNullException(nameof(outputHandler));
    private readonly IAccountRepository _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IServiceBusClient _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));

    public async Task ExecuteAsync(TransferInput input, CancellationToken cancellationToken = default)
    {
        var originAccount = await _accountRepository.GetAsync(input.OriginAccountId);
        if (originAccount == null)
        {
            _outputHandler.Error($"The account {input.OriginAccountId} does not exist or is already closed.");
            return;
        }

        var destinationAccount = await _accountRepository.GetAsync(input.DestinationAccountId);
        if (destinationAccount == null)
        {
            _outputHandler.Error($"The account {input.DestinationAccountId} does not exist or is already closed.");
            return;
        }

        var debit = originAccount.Withdraw(_entityFactory, input.Amount);
        var credit = destinationAccount.Deposit(_entityFactory, input.Amount);

        if(debit == null)
        {
            _outputHandler.Error("debit.Error");
            return;
        }

        await _accountRepository.UpdateAsync(originAccount, debit, cancellationToken);
        await _accountRepository.UpdateAsync(destinationAccount, credit, cancellationToken);

        // Publish the event to the enterprise service bus
        await _serviceBus.PublishEventAsync(new Contracts.Events.TransferCompleted() { OriginalAccountId = originAccount.Id, DestinationAccountId = destinationAccount.Id, Amount = input.Amount.ToMoney().ToDecimal() }, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);

        TransferOutput output = new TransferOutput(
                                                   debit,
                                                   originAccount.GetCurrentBalance(),
                                                   input.OriginAccountId,
                                                   input.DestinationAccountId);

        _outputHandler.Default(output);
    }
}