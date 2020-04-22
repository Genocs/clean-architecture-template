namespace Genocs.MicroserviceLight.Template.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Genocs.Microservices.Common.Types;
    using Genocs.MicroserviceLight.Template.Domain;
    using Genocs.MicroserviceLight.Template.Queries;

    public interface IFooTemplatesRepository
    {
        Task<FooTemplate> GetAsync(Guid id);
        Task<PagedResult<FooTemplate>> BrowseAsync(BrowseFooTemplates query);
        Task AddAsync(FooTemplate fooTemplate);
        Task UpdateAsync(FooTemplate fooTemplate);
        Task DeleteAsync(Guid id);
    }
}