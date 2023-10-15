namespace Genocs.MicroserviceLight.Template.Infrastructure.PersistenceLayer.InMemory
{
    using Application.Services;
    using System.Threading.Tasks;

    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly GenocsContext _context;

        public UnitOfWork(GenocsContext context)
            => _context = context;

        public async Task<int> Save()
            => await Task.FromResult<int>(0);
    }
}