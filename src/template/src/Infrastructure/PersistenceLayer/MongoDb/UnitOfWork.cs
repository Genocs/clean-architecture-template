using Genocs.CleanArchitecture.Template.Application.Services;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb;

public sealed class UnitOfWork(IMongoContext context) : IUnitOfWork, IDisposable
{
    private readonly IMongoContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync();

    private bool _disposed = false;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            _disposed = true;
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
