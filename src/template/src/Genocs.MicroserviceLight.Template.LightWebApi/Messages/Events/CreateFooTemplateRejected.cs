namespace Genocs.MicroserviceLight.Template.Messages.Events
{
    using System;
    using Genocs.Microservices.Common.Messages;
    using Newtonsoft.Json;

    public class CreateFooTemplateRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        [JsonConstructor]
        public CreateFooTemplateRejected(Guid id, string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}