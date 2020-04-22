namespace Genocs.MicroserviceLight.Template.Application.UseCases
{
    using Genocs.MicroserviceLight.Template.Application.Boundaries.Register;
    using Genocs.MicroserviceLight.Template.Application.Repositories;
    using Genocs.MicroserviceLight.Template.Application.Services;
    using Genocs.MicroserviceLight.Template.Domain;
    using Genocs.MicroserviceLight.Template.Domain.Accounts;
    using System.Threading.Tasks;

    public sealed class Register : IUseCase
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IOutputPort _outputHandler;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Register(
            IEntityFactory entityFactory,
            IOutputPort outputHandler,
            ICustomerRepository customerRepository,
            IAccountRepository accountRepository,
            IUnitOfWork unityOfWork)
        {
            _entityFactory = entityFactory;
            _outputHandler = outputHandler;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unityOfWork;
        }

        public async Task Execute(RegisterInput input)
        {
            if (input == null)
            {
                _outputHandler.Error("Input is null.");
                return;
            }

            var customer = _entityFactory.NewCustomer(input.SSN, input.Name);
            var account = _entityFactory.NewAccount(customer);

            ICredit credit = account.Deposit(_entityFactory, input.InitialAmount);
            if (credit == null)
            {
                _outputHandler.Error("An error happened when depositing the amount.");
                return;
            }

            customer.Register(account);

            await _customerRepository.Add(customer);
            await _accountRepository.Add(account, credit);
            await _unitOfWork.Save();

            RegisterOutput output = new RegisterOutput(customer, account);
            _outputHandler.Standard(output);
        }
    }
}