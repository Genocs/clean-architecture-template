
namespace Genocs.MicroserviceLight.Template.Messages.Commands
{
    using System;
    using Genocs.Microservices.Common.Messages;
    using Newtonsoft.Json;

    public class CreateFooTemplate : ICommand
    {
        public Guid Id { get; }
        public string Code { get; }
        public string Label { get; }

        [JsonConstructor]
        public CreateFooTemplate(Guid id,
            string code,
            string label)
        {
            Id = id;
            Code = code;
            Label = label;
        }
    }
}