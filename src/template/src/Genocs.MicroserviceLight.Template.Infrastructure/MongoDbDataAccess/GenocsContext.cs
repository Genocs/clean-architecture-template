namespace Genocs.MicroserviceLight.Template.Infrastructure.MongoDbDataAccess
{
    using Microsoft.Extensions.Configuration;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Conventions;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public sealed class GenocsContext : IMongoContext
    {
        private readonly IMongoDatabase _database = null;

        private readonly List<Func<Task>> _commands;

        public MongoClient MongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }


        public GenocsContext(IConfiguration configuration)
        {
            // Set Guid to CSharp style (with dash -)
            BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;

            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();

            RegisterConventions();

            // Configure mongo (You can inject the config, just to simplify)
            MongoClient = new MongoClient(Environment.GetEnvironmentVariable("MONGOCONNECTION") ?? configuration.GetSection("MongoSettings").GetSection("Connection").Value);

            _database = MongoClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASENAME") ?? configuration.GetSection("MongoSettings").GetSection("DatabaseName").Value);

        }

        private void RegisterConventions()
        {
            var pack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true),
                new IgnoreIfDefaultConvention(true)
            };

            ConventionRegistry.Register("Genocs Solution Conventions", pack, t => true);
        }

        public async Task<int> SaveChanges()
        {
            using (Session = await MongoClient.StartSessionAsync())
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        private bool _disposed = false;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    while (Session != null && Session.IsInTransaction)
                        Thread.Sleep(TimeSpan.FromMilliseconds(100));
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func)
        {
            _commands.Add(func);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }


        public IMongoCollection<Customer> Customers
        {
            get
            {
                return _database.GetCollection<Customer>("Customers");
            }
        }

        public IMongoCollection<Account> Accounts
        {
            get
            {
                return _database.GetCollection<Account>("Accounts");
            }
        }

        public IMongoCollection<Credit> Credits
        {
            get
            {
                return _database.GetCollection<Credit>("Credits");
            }
        }
        public IMongoCollection<Debit> Debits
        {
            get
            {
                return _database.GetCollection<Debit>("Debits");
            }
        }
    }
}
