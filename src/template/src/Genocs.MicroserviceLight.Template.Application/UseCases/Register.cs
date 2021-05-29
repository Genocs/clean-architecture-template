namespace Genocs.MicroserviceLight.Template.Application.UseCases
{
    using Application.Boundaries.Register;
    using Application.Repositories;
    using Application.Services;
    using Domain;
    using Domain.Accounts;
    using Shared.Events;
    using System.Threading.Tasks;

    public sealed class Register : IUseCase
    {
        private readonly IEntityFactory _entityFactory;
        private readonly IOutputPort _outputHandler;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceBusClient _serviceBus;

        public Register(
            IEntityFactory entityFactory,
            IOutputPort outputHandler,
            ICustomerRepository customerRepository,
            IAccountRepository accountRepository,
            IUnitOfWork unityOfWork,
            IServiceBusClient serviceBus)
        {
            _entityFactory = entityFactory;
            _outputHandler = outputHandler;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _unitOfWork = unityOfWork;
            _serviceBus = serviceBus;
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

            // Call to an external Web Api

            await _customerRepository.Add(customer);
            await _accountRepository.Add(account, credit);

            // Publish the event to the enterprice service bus
            await _serviceBus.PublishEventAsync(new RegistrationCompleted() { CustomerId = customer.Id, AccountId = account.Id, CreditId = credit.Id });


            await _unitOfWork.Save();


            RegisterOutput output = new RegisterOutput(customer, account);
            _outputHandler.Standard(output);
        }
    }
}