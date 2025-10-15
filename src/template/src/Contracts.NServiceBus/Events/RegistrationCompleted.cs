namespace Genocs.CleanArchitecture.Template.ContractsNServiceBus.Events;

/// <summary>
/// This is a copy of <see cref="Genocs.CleanArchitecture.Template.Contracts.Events.RegistrationCompleted"/> class
/// it's needed because NServiceBus event messages must implement <see cref="NServiceBus.IEvent"/> interface.
/// </summary>
public class RegistrationCompleted : Genocs.CleanArchitecture.Template.Contracts.Events.RegistrationCompleted, NServiceBus.IEvent;
