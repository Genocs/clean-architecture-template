namespace Genocs.MicroserviceLight.Template.Application.UseCases
{
    using Application.Boundaries.CloseAccount;
    using Application.Repositories;
    using Domain.Accounts;
    using Application.Services;
    using System.Threading.Tasks;

    public sealed class CloseAccount : IUseCase
    {
        private readonly IOutputPort _outputHandler;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceBusClient _serviceBus;

        public CloseAccount(
            IOutputPort outputHandler,
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork,
            IServiceBusClient serviceBus)
        {
            _outputHandler = outputHandler;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _serviceBus = serviceBus;
        }

        public async Task Execute(CloseAccountInput closeAccountInput)
        {
            IAccount account = await _accountRepository.Get(closeAccountInput.AccountId);
            if (account == null)
            {
                _outputHandler.Error($"The account '{closeAccountInput.AccountId}' does not exist or is already closed.");
                return;
            }

            if (account.IsClosingAllowed())
            {
                await _accountRepository.Delete(account);
                // Publish the event to the enterprice service bus
                await _serviceBus.PublishEventAsync(new Shared.Events.CloseAccountCompleted() { AccountId = account.Id });

                await _unitOfWork.Save();
            }

            var closeAccountOutput = new CloseAccountOutput(account);
            _outputHandler.Default(closeAccountOutput);
        }
    }
}