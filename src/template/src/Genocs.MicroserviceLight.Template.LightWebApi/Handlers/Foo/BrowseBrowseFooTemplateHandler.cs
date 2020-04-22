namespace Genocs.MicroserviceLight.Template.Handlers.Foo
{
    using System.Linq;
    using System.Threading.Tasks;
    using Genocs.Microservices.Common.Handlers;
    using Genocs.Microservices.Common.Types;
    using Genocs.MicroserviceLight.Template.Dto;
    using Genocs.MicroserviceLight.Template.Queries;
    using Genocs.MicroserviceLight.Template.Repositories;

    public class BrowseFooTemplateHandler : IQueryHandler<BrowseFooTemplates, PagedResult<FooTemplateDto>>
    {
        private readonly IFooTemplatesRepository _fooTemplatesRepository;

        public BrowseFooTemplateHandler(IFooTemplatesRepository fooTemplatesRepository)
            => _fooTemplatesRepository = fooTemplatesRepository;

        public async Task<PagedResult<FooTemplateDto>> HandleAsync(BrowseFooTemplates query)
        {
            var pagedResult = await _fooTemplatesRepository.BrowseAsync(query);

            // Use mapper
            var fooTemplates = pagedResult.Items.Select(c => new FooTemplateDto
            {
                Id = c.Id,
                Code = c.Code,
                Label = c.Label
            });

            return PagedResult<FooTemplateDto>.From(pagedResult, fooTemplates);
        }
    }
}