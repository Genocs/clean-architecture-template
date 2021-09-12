using Genocs.MicroservicesLight.Template.ParticularShared.Commands;
using NServiceBus;
using NServiceBus.Logging;
using System.Threading.Tasks;

namespace Genocs.MicroserviceLight.Template.BusWorker.Handlers.Saga
{
    public class CreditCardRefundPolicy : Saga<CreditCardRefundPolicyData>,
                                            IAmStartedByMessages<TransactionLoaded>,
                                            IHandleMessages<TransactionUnloaded>,
                                            IHandleMessages<FeeUnloaded>,
                                            IHandleMessages<UnloadCompleted>,
                                            IHandleMessages<RefundCompleted>
    {
        static readonly ILog log = LogManager.GetLogger<CreditCardRefundPolicy>();

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CreditCardRefundPolicyData> mapper)
        {
            mapper.ConfigureMapping<TransactionLoaded>(message => message.TransactionId)
                    .ToSaga(sagaData => sagaData.TransactionId);

            mapper.ConfigureMapping<TransactionUnloaded>(message => message.TransactionId)
                    .ToSaga(sagaData => sagaData.TransactionId);

            mapper.ConfigureMapping<FeeUnloaded>(message => message.TransactionId)
                    .ToSaga(sagaData => sagaData.TransactionId);

            mapper.ConfigureMapping<UnloadCompleted>(message => message.TransactionId)
                    .ToSaga(sagaData => sagaData.TransactionId);

            mapper.ConfigureMapping<RefundCompleted>(message => message.TransactionId)
                    .ToSaga(sagaData => sagaData.TransactionId);

        }

        public Task Handle(TransactionLoaded message, IMessageHandlerContext context)
        {
            log.Info($"Received TransactionLoaded, TransactionId = {message.TransactionId}");
            return Task.CompletedTask;
        }

        public async Task Handle(TransactionUnloaded message, IMessageHandlerContext context)
        {
            log.Info($"Received TransactionUnloaded, TransactionId = {message.TransactionId}");

            Data.TransactionUnloaded = true;
            await context.SendLocal(new UnloadCompleted() { TransactionId = Data.TransactionId });

            log.Info($"Transaction unloaded successfully, TransactionId = {message.TransactionId}");

        }

        public async Task Handle(FeeUnloaded message, IMessageHandlerContext context)
        {
            log.Info($"Received FeeUnloaded, TransactionId = {message.TransactionId}");

            Data.FeeUnloaded = true;
            await context.SendLocal(new UnloadCompleted() { TransactionId = Data.TransactionId });


            log.Info($"Fee unloaded successfully, TransactionId = {message.TransactionId}");

        }

        public Task Handle(UnloadCompleted message, IMessageHandlerContext context)
        {
            if (!Data.TransactionUnloaded || !Data.FeeUnloaded)
            {
                return Task.CompletedTask;
            }

            log.Info($"Received UnloadCompleted, TransactionId = {message.TransactionId}");

            log.Info($"Calling the External Service crediting the customer, TransactionId = {message.TransactionId}");

            return Task.CompletedTask;
        }

        public Task Handle(RefundCompleted message, IMessageHandlerContext context)
        {
            log.Info($"Received RefundCompleted, TransactionId = {message.TransactionId}");

            log.Info($"Everything went well. Update the account status, TransactionId = {message.TransactionId}");

            MarkAsComplete();
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// The SagaData. This class contains Saga State Machine so it is not shared 
    /// </summary>
    public class CreditCardRefundPolicyData : ContainSagaData
    {
        public string TransactionId { get; set; }
        public bool TransactionUnloaded { get; set; }
        public bool FeeUnloaded { get; set; }
        public bool TransactionRefunded { get; set; }
    }
}
