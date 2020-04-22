namespace Genocs.MicroserviceLight.Template.Handlers.Foo
{
    using System.Threading.Tasks;
    using Genocs.Microservices.Common.Handlers;
    using Genocs.MicroserviceLight.Template.Dto;
    using Genocs.MicroserviceLight.Template.Queries;
    using Genocs.MicroserviceLight.Template.Repositories;

    public class GetFooTemplateHandler : IQueryHandler<GetFooTemplatesDto, FooTemplateDto>
    {
        private readonly IFooTemplatesRepository _fooTemplatesRepository;

        public GetFooTemplateHandler(IFooTemplatesRepository fooTemplatesRepository)
        {
            _fooTemplatesRepository = fooTemplatesRepository;
        }

        public async Task<FooTemplateDto> HandleAsync(GetFooTemplatesDto query)
        {
            var fooTemplate = await _fooTemplatesRepository.GetAsync(query.Id);

            return fooTemplate == null ? null : new FooTemplateDto
            {
                Id = fooTemplate.Id,
                Code = fooTemplate.Code,
                Label = fooTemplate.Label,
                CreatedAt = fooTemplate.CreatedAt
            };
        }
    }
}