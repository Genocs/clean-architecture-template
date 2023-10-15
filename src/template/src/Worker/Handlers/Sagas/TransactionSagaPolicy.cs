using Genocs.CleanArchitecture.Template.Shared.Particular.TransactionSaga;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Sagas;

namespace Genocs.CleanArchitecture.Template.Worker.Handlers.Sagas;


/// <summary>
/// This is the TransactionSagaPolicy body
/// </summary>
public class TransactionSagaPolicy : Saga<TransactionSagaData>,
                                    IAmStartedByMessages<TransactionLoaded>,
                                    IHandleMessages<TransactionUnloaded>,
                                    IHandleMessages<RedemptionStarted>,
                                    IHandleMessages<RedemptionCompleted>,
                                    IHandleMessages<RedemptionRejected>,
                                    IHandleSagaNotFound
{
    static ILog _log = LogManager.GetLogger<TransactionSagaPolicy>();


    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<TransactionSagaData> mapper)
    {
        mapper.ConfigureMapping<TransactionLoaded>(message => message.RequestId)
            .ToSaga(sagaData => sagaData.RequestId);

        mapper.ConfigureMapping<TransactionUnloaded>(message => message.RequestId)
            .ToSaga(sagaData => sagaData.RequestId);

        mapper.ConfigureMapping<RedemptionStarted>(message => message.RequestId)
            .ToSaga(sagaData => sagaData.RequestId);

        mapper.ConfigureMapping<RedemptionCompleted>(message => message.RequestId)
            .ToSaga(sagaData => sagaData.RequestId);

        mapper.ConfigureMapping<RedemptionRejected>(message => message.RequestId)
            .ToSaga(sagaData => sagaData.RequestId);
    }

    public Task Handle(TransactionLoaded message, IMessageHandlerContext context)
    {
        _log.Info($"TransactionLoaded received message with RequestId: '{message.RequestId}'.");
        Data.TransactionStatus = "TransactionLoaded";
        Data.Property1 = "Property1 Done";
        return Task.CompletedTask;
    }

    public Task Handle(TransactionUnloaded message, IMessageHandlerContext context)
    {
        _log.Info($"TransactionUnloaded received message with RequestId: '{message.RequestId}'.");

        if (Data.TransactionStatus != "TransactionLoaded")
        {
            Data.Property2 = "INVALID STATUS";
            _log.Error($"TransactionUnloaded INVALID STATUS, RequestId: '{message.RequestId}'.");
            return Task.CompletedTask;
        }

        Data.TransactionStatus = "TransactionUnloaded";
        return Task.CompletedTask;
    }

    public Task Handle(RedemptionStarted message, IMessageHandlerContext context)
    {
        _log.Info($"RedemptionStarted received message with RequestId: '{message.RequestId}'.");

        if (Data.TransactionStatus != "TransactionUnloaded")
        {
            Data.Property2 = "INVALID STATUS";
            _log.Error($"TransactionUnloaded INVALID STATUS, RequestId: '{message.RequestId}'.");
            return Task.CompletedTask;
        }

        Data.TransactionStatus = "RedemptionStarted";
        return Task.CompletedTask;
    }

    public Task Handle(RedemptionCompleted message, IMessageHandlerContext context)
    {
        _log.Info($"RedemptionCompleted received message with RequestId: '{message.RequestId}'.");

        if (Data.TransactionStatus != "RedemptionStarted")
        {
            Data.Property2 = "INVALID STATUS";
            _log.Error($"TransactionUnloaded INVALID STATUS, RequestId: '{message.RequestId}'.");
            return Task.CompletedTask;
        }
        MarkAsComplete();
        return Task.CompletedTask;
    }

    public Task Handle(RedemptionRejected message, IMessageHandlerContext context)
    {
        _log.Info($"RedemptionRejected received message with RequestId: '{message.RequestId}'.");
        Data.TransactionStatus = "RedemptionRejected";
        return Task.CompletedTask;
    }

    /// <summary>
    /// This handle IHandleSagaNotFound 
    /// </summary>
    /// <param name="message"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task Handle(object message, IMessageProcessingContext context)
    {
        var evt1 = message as TransactionLoaded;
        if (evt1 is not null)
        {
            _log.Error($"Received SagaNotFound with Property1SetEvent. RequestId: '{evt1.RequestId}'");
        }
        var evt2 = message as TransactionUnloaded;

        if (evt2 is not null)
        {
            _log.Error($"Received SagaNotFound with Property2SetEvent. RequestId: '{evt2.RequestId}'");
        }
        else
        {
            _log.Error($"Received SagaNotFound");
        }

        return Task.CompletedTask;
    }
}
