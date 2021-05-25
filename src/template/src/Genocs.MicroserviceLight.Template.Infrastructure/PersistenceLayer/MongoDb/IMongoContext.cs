namespace Genocs.MicroserviceLight.Template.Infrastructure.PersistenceLayer.MongoDb
{
    using Domain;
    using MongoDB.Driver;
    using System;
    using System.Threading.Tasks;

    public interface IMongoContext : IDisposable
    {
        MongoClient MongoClient { get; set; }
        IClientSessionHandle Session { get; set; }
        Task<int> SaveChangesAsync();
        void AddCommand(Func<Task> func);
        IMongoCollection<T> GetCollection<T>(string name) where T : IEntity;
    }
}
