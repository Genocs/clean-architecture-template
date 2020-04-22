namespace Genocs.MicroserviceLight.Template.Dto
{
    using System;

    public class FooTemplateDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Label { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}