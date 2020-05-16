namespace Genocs.MicroserviceLight.Template.Application.UseCases
{
    using Application.Boundaries.Withdraw;
    using Application.Repositories;
    using Application.Services;
    using Domain;
    using Domain.Accounts;
    using System.Threading.Tasks;

    public sealed class Withdraw : IUseCase
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IOutputPort _outputHandler;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Withdraw(
            IEntityFactory entityFactory,
            IOutputPort outputHandler,
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork)
        {
            _entityFactory = entityFactory;
            _outputHandler = outputHandler;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(WithdrawInput input)
        {
            IAccount account = await _accountRepository.Get(input.AccountId);
            if (account == null)
            {
                _outputHandler.Error($"The account {input.AccountId} does not exist or is already closed.");
                return;
            }

            IDebit debit = account.Withdraw(_entityFactory, input.Amount);

            if (debit == null)
            {
                _outputHandler.Error($"The account {input.AccountId} does not have enough funds to withdraw {input.Amount}.");
                return;
            }

            await _accountRepository.Update(account, debit);
            await _unitOfWork.Save();

            WithdrawOutput output = new WithdrawOutput(
                debit,
                account.GetCurrentBalance()
            );

            _outputHandler.Default(output);
        }
    }
}