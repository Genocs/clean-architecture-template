namespace Genocs.MicroserviceLight.Template.Application.UseCases
{
    using Genocs.MicroserviceLight.Template.Application.Boundaries.Transfer;
    using Genocs.MicroserviceLight.Template.Application.Repositories;
    using Genocs.MicroserviceLight.Template.Application.Services;
    using Genocs.MicroserviceLight.Template.Domain;
    using Genocs.MicroserviceLight.Template.Domain.Accounts;
    using System.Threading.Tasks;

    public sealed class Transfer : IUseCase
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IOutputPort _outputHandler;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Transfer(
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

        public async Task Execute(TransferInput input)
        {
            IAccount originAccount = await _accountRepository.Get(input.OriginAccountId);
            if (originAccount == null)
            {
                _outputHandler.Error($"The account {input.OriginAccountId} does not exist or is already closed.");
                return;
            }

            IAccount destinationAccount = await _accountRepository.Get(input.DestinationAccountId);
            if (destinationAccount == null)
            {
                _outputHandler.Error($"The account {input.DestinationAccountId} does not exist or is already closed.");
                return;
            }

            IDebit debit = originAccount.Withdraw(_entityFactory, input.Amount);
            ICredit credit = destinationAccount.Deposit(_entityFactory, input.Amount);

            await _accountRepository.Update(originAccount, debit);
            await _accountRepository.Update(destinationAccount, credit);
            await _unitOfWork.Save();

            TransferOutput output = new TransferOutput(
                debit,
                originAccount.GetCurrentBalance(),
                input.OriginAccountId,
                input.DestinationAccountId);

            _outputHandler.Default(output);
        }
    }
}