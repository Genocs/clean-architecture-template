namespace Genocs.MicroserviceLight.Template.Infrastructure.PersistenceLayer.EntityFramework
{
    using Application.Services;
    using System;
    using System.Threading.Tasks;

    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly GenocsContext _context;

        public UnitOfWork(GenocsContext context)
            => _context = context;

        public async Task<int> Save()
        {
            int affectedRows = await _context.SaveChangesAsync();
            return affectedRows;
        }

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