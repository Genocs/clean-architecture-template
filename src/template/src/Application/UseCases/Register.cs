using Genocs.CleanArchitecture.Template.Application.Boundaries.Registers;
using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;
using Genocs.CleanArchitecture.Template.Contracts.Events;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class Register(IEntityFactory entityFactory,
    IOutputPort outputHandler,
    ICustomerRepository customerRepository,
    IAccountRepository accountRepository,
    IUnitOfWork unityOfWork,
    IServiceBusClient serviceBus)
    : IUseCase
{
    private readonly IEntityFactory _entityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory));
    private readonly IOutputPort _outputHandler = outputHandler ?? throw new ArgumentNullException(nameof(outputHandler));
    private readonly ICustomerRepository _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    private readonly IAccountRepository _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    private readonly IUnitOfWork _unitOfWork = unityOfWork ?? throw new ArgumentNullException(nameof(unityOfWork));
    private readonly IServiceBusClient _serviceBus = serviceBus ?? throw new ArgumentNullException(nameof(serviceBus));

    public async Task ExecuteAsync(RegisterInput input, CancellationToken cancellationToken = default)
    {
        if (input == null)
        {
            _outputHandler.Error("Input is null.");
            return;
        }

        var customer = _entityFactory.NewCustomer(input.SSN, input.Name);
        var account = _entityFactory.NewAccount(customer);

        var credit = account.Deposit(_entityFactory, input.InitialAmount);
        if (credit == null)
        {
            _outputHandler.Error("An error happened when depositing the amount.");
            return;
        }

        customer.Register(account);

        // Call to an external Web Api
        await _customerRepository.AddAsync(customer, cancellationToken);
        await _accountRepository.AddAsync(account, credit, cancellationToken);

        // Publish the event to the enterprise service bus
#if NServiceBus
        await _serviceBus.PublishEventAsync(new Genocs.CleanArchitecture.Template.ContractsNServiceBus.Events.RegistrationCompleted() { CustomerId = customer.Id, AccountId = account.Id, CreditId = credit.Id });
#else
        await _serviceBus.PublishEventAsync(new RegistrationCompleted() { CustomerId = customer.Id, AccountId = account.Id, CreditId = credit.Id });
#endif

        await _unitOfWork.Save();

        RegisterOutput output = new RegisterOutput(customer, account);
        _outputHandler.Standard(output);
    }
}