namespace Genocs.MicroserviceLight.Template.Domain
{
    using System;
    using Genocs.Microservices.Common.Types;

    public class FooTemplate : IIdentifiable
    {
        public Guid Id { get; private set; }
        public string Code { get; private set; }
        public string Label { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected FooTemplate()
        {
        }

        public FooTemplate(Guid id,
            string code,
            string label)
        {
            Id = id;
            Code = code;
            Label = label;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string label)
        {
            Label = label;
        }
    }
}