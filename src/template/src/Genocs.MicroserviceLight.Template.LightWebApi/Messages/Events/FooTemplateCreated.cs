namespace Genocs.MicroserviceLight.Template.Messages.Events
{
    using System;
    using Genocs.Microservices.Common.Messages;
    using Newtonsoft.Json;

    [MessageNamespace("template")]
    public class FooTemplateCreated : IEvent
    {
        public Guid Id { get; }
        public string Code { get; }
        public string Label { get; }

        [JsonConstructor]
        public FooTemplateCreated(Guid id,
            string code,
            string label)
        {
            Id = id;
            Code = code;
            Label = label;
        }
    }
}