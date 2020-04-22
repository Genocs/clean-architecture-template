namespace Genocs.MicroserviceLight.Template.Queries
{
    using Genocs.Microservices.Common.Types;
    using Genocs.MicroserviceLight.Template.Dto;

    public class BrowseFooTemplates : PagedQueryBase, IQuery<PagedResult<FooTemplateDto>>
    {
        public string Code { get; set; }
    }
}