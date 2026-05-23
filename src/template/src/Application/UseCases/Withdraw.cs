using Genocs.CleanArchitecture.Template.Application.Boundaries.Withdraws;
using Genocs.CleanArchitecture.Template.Application.Interfaces;
using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class Withdraw(
            IEntityFactory entityFactory,
            IOutputPort<WithdrawOutput> outputHandler,
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork,
            IServiceBusClient serviceBus) : IUseCase<WithdrawInput>
{
    private readonly IEntityFactory _entityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory));
    private readonly IOutputPort<WithdrawOutput> _outputHandler = outputHandler ?? throw new ArgumentNullException(nameof(outputHandler));
    private readonly IAccountRepository _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IServiceBusClient _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));

    public async Task ExecuteAsync(WithdrawInput input, CancellationToken cancellationToken = default)
    {
        var account = await _accountRepository.GetAsync(input.AccountId, cancellationToken);
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

        await _accountRepository.UpdateAsync(account, debit);

        await _serviceBus.PublishEventAsync(new Contracts.Events.WithdrawCompleted() { AccountId = input.AccountId, Amount = input.Amount.ToMoney().ToDecimal() }, cancellationToken);

        await _unitOfWork.SaveAsync(cancellationToken);

        WithdrawOutput output = new WithdrawOutput(
            debit,
            account.GetCurrentBalance());

        _outputHandler.Default(output);
    }
}