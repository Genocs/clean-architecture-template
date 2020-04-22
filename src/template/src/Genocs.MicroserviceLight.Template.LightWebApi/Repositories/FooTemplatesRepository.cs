namespace Genocs.MicroserviceLight.Template.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Genocs.Microservices.Common.Mongo;
    using Genocs.Microservices.Common.Types;
    using Genocs.MicroserviceLight.Template.Domain;
    using Genocs.MicroserviceLight.Template.Queries;

    public class FooTemplatesRepository : IFooTemplatesRepository
    {
        private readonly IMongoRepository<FooTemplate> _repository;

        public FooTemplatesRepository(IMongoRepository<FooTemplate> repository)
            => _repository = repository;

        public Task<PagedResult<FooTemplate>> BrowseAsync(BrowseFooTemplates query)
           => _repository.BrowseAsync(_ => true, query);

        public Task<FooTemplate> GetAsync(Guid id)
            => _repository.GetAsync(id);

        public Task AddAsync(FooTemplate template)
            => _repository.AddAsync(template);

        public Task UpdateAsync(FooTemplate template)
            => _repository.UpdateAsync(template);

        public Task DeleteAsync(Guid id)
            => _repository.DeleteAsync(id);
    }
}