namespace Genocs.MicroserviceLight.Template.Infrastructure.MongoDbDataAccess
{
    using MongoDB.Driver;
    using System;
    using System.Threading.Tasks;

    public interface IMongoContext : IDisposable
    {
        MongoClient MongoClient { get; set; }
        IClientSessionHandle Session { get; set; }
        Task<int> SaveChanges();
        void AddCommand(Func<Task> func);
        IMongoCollection<T> GetCollection<T>(string name);
        IMongoCollection<Customer> Customers { get; }
        public IMongoCollection<Account> Accounts { get; }
        public IMongoCollection<Credit> Credits { get; }
        public IMongoCollection<Debit> Debits { get; }
    }
}
