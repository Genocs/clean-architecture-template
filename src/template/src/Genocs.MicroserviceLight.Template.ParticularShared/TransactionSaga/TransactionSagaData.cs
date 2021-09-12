using NServiceBus;

namespace Genocs.MicroservicesLight.Template.ParticularShared.TransactionSaga
{
    /// <summary>
    /// This is my simple SagaDataObject
    /// </summary>
    public class TransactionSagaData : ContainSagaData
    {
        public string RequestId { get; set; }
        public string TransactionStatus { get; set; }

        public string Property1 { get; set; }
        public string Property2 { get; set; }
    }
}
