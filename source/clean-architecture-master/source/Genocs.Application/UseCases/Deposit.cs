namespace Genocs.Application.UseCases
{
    using Genocs.Application.Boundaries.Deposit;
    using Genocs.Application.Repositories;
    using Genocs.Application.Services;
    using Genocs.Domain;
    using Genocs.Domain.Accounts;
    using System.Threading.Tasks;

    public sealed class Deposit : IUseCase
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IOutputPort _outputHandler;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Deposit(
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

        public async Task Execute(DepositInput input)
        {
            IAccount account = await _accountRepository.Get(input.AccountId);
            if (account == null)
            {
                _outputHandler.Error($"The account {input.AccountId} does not exist or is already closed.");
                return;
            }

            ICredit credit = account.Deposit(_entityFactory, input.Amount);

            await _accountRepository.Update(account, credit);
            await _unitOfWork.Save();

            DepositOutput output = new DepositOutput(
                credit,
                account.GetCurrentBalance()
            );

            _outputHandler.Default(output);
        }
    }
}