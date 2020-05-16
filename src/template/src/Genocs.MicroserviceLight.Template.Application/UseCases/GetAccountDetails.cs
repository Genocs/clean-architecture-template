namespace Genocs.MicroserviceLight.Template.Application.UseCases
{
    using Application.Boundaries.GetAccountDetails;
    using Application.Repositories;
    using Domain.Accounts;
    using System.Threading.Tasks;

    public sealed class GetAccountDetails : IUseCase
    {
        private readonly IOutputPort _outputHandler;
        private readonly IAccountRepository _accountRepository;

        public GetAccountDetails(
            IOutputPort outputHandler,
            IAccountRepository accountRepository)
        {
            _outputHandler = outputHandler;
            _accountRepository = accountRepository;
        }

        public async Task Execute(GetAccountDetailsInput input)
        {
            IAccount account = await _accountRepository.Get(input.AccountId);

            if (account == null)
            {
                _outputHandler.NotFound($"The account {input.AccountId} does not exist or is not processed yet.");
                return;
            }

            GetAccountDetailsOutput output = new GetAccountDetailsOutput(account);
            _outputHandler.Default(output);
        }
    }
}