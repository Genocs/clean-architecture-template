namespace Genocs.MicroserviceLight.Template.Infrastructure.MongoDbDataAccess
{
    using Application.Services;
    using System;
    using System.Threading.Tasks;

    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IMongoContext _context;

        public UnitOfWork(IMongoContext context)
            => _context = context;

        public async Task<int> Save()
            => await _context.SaveChangesAsync();

        private bool _disposed = false;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
