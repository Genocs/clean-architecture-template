using Genocs.CleanArchitecture.Template.Application.Services;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.EntityFramework;

public sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly GenocsContext _context;

    public UnitOfWork(GenocsContext context)
        => _context = context;

    public async Task<int> Save()
        => await _context.SaveChangesAsync();

    private bool _disposed;

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

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}