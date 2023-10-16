using Genocs.CleanArchitecture.Template.Domain;
using MongoDB.Driver;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb;

public interface IMongoContext : IDisposable
{
    MongoClient MongoClient { get; set; }
    IClientSessionHandle Session { get; set; }
    Task<int> SaveChangesAsync();
    void AddCommand(Func<Task> func);
    IMongoCollection<T> GetCollection<T>(string name)
        where T : IEntity;
}
