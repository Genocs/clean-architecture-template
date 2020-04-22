namespace Genocs.MicroserviceLight.Template.Queries
{
    using System;
    using Genocs.Microservices.Common.Types;
    using Genocs.MicroserviceLight.Template.Dto;

    public class GetFooTemplatesDto : IQuery<FooTemplateDto>
    {
        public Guid Id { get; set; }
    }
}