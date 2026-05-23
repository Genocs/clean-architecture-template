using Genocs.CleanArchitecture.Template.Contracts.Events;

namespace Genocs.CleanArchitecture.Template.ContractsNServiceBus.Events;

public class ParticularRegistrationCompleted : RegistrationCompleted, NServiceBus.IEvent;
