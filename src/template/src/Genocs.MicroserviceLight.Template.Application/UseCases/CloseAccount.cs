namespace Genocs.MicroserviceLight.Template.Application.UseCases
{
    using Application.Boundaries.CloseAccount;
    using Application.Repositories;
    using Domain.Accounts;
    using System.Threading.Tasks;

    public sealed class CloseAccount : IUseCase
    {
        private readonly IOutputPort _outputHandler;
        private readonly IAccountRepository _accountRepository;

        public CloseAccount(
            IOutputPort outputHandler,
            IAccountRepository accountRepository)
        {
            _outputHandler = outputHandler;
            _accountRepository = accountRepository;
        }

        public async Task Execute(CloseAccountInput closeAccountInput)
        {
            IAccount account = await _accountRepository.Get(closeAccountInput.AccountId);
            if (account == null)
            {
                _outputHandler.Error($"The account {closeAccountInput.AccountId} does not exist or is already closed.");
                return;
            }

            if (account.IsClosingAllowed())
            {
                await _accountRepository.Delete(account);
            }

            var closeAccountOutput = new CloseAccountOutput(account);
            _outputHandler.Default(closeAccountOutput);
        }
    }
}