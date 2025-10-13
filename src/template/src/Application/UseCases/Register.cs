using Genocs.CleanArchitecture.Template.Application.Boundaries.Registers;
using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;
using Genocs.CleanArchitecture.Template.Shared.Events;

namespace Genocs.CleanArchitecture.Template.Application.UseCases;

public sealed class Register(IEntityFactory entityFactory,
    IOutputPort outputHandler,
    ICustomerRepository customerRepository,
    IAccountRepository accountRepository,
    IUnitOfWork unityOfWork,
    IServiceBusClient serviceBus)
    : IUseCase
{
    private readonly IEntityFactory _entityFactory = entityFactory;
    private readonly IOutputPort _outputHandler = outputHandler;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IAccountRepository _accountRepository = accountRepository;
    private readonly IUnitOfWork _unitOfWork = unityOfWork;
    private readonly IServiceBusClient _serviceBus = serviceBus;

    public async Task ExecuteAsync(RegisterInput input)
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
        await _customerRepository.Add(customer);
        await _accountRepository.Add(account, credit);

        // Publish the event to the enterprise service bus
        await _serviceBus.PublishEventAsync(new RegistrationCompleted() { CustomerId = customer.Id, AccountId = account.Id, CreditId = credit.Id });

        await _unitOfWork.Save();

        RegisterOutput output = new RegisterOutput(customer, account);
        _outputHandler.Standard(output);
    }
}