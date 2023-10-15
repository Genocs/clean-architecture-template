﻿using NServiceBus;

namespace Genocs.CleanArchitecture.Template.Shared.Particular.TransactionSaga;

/// <summary>
/// This event happend when the system receive an Unload
/// </summary>
public class RedemptionCompleted : IEvent
{
    public string RequestId { get; set; }
    public string TransactionId { get; set; }
}
