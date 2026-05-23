using Genocs.Common.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;
using Genocs.CleanArchitecture.Template.Domain.ValueObjects;
using Genocs.CleanArchitecture.Template.Domain.Customers;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb;

public sealed class GenocsContext : IMongoContext
{
    private readonly IMongoDatabase _database;

    private readonly List<Func<Task>> _commands;

    public MongoClient MongoClient { get; set; }
    private IClientSessionHandle? Session { get; set; }

    public GenocsContext(IConfiguration configuration)
    {
        // Every command will be stored and it'll be processed at SaveChanges
        _commands = new List<Func<Task>>();

        // Configure mongo (You can inject the config, just to simplify)
        MongoClient = new MongoClient(Environment.GetEnvironmentVariable("MONGOCONNECTION") ?? configuration.GetSection("MongoSettings").GetSection("Connection").Value);

        _database = MongoClient.GetDatabase(Environment.GetEnvironmentVariable("DATABASENAME") ?? configuration.GetSection("MongoSettings").GetSection("DatabaseName").Value);
    }

    public static void RegisterConventions()
    {
        // Set Guid to CSharp style (with dash -)
        // Only register if not already registered
        try
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));
        }
        catch (BsonSerializationException)
        {
            // Serializer already registered, ignore
        }

        // Register BsonClassMap for Name value object
        if (!BsonClassMap.IsClassMapRegistered(typeof(Name)))
        {
            try
            {
                BsonClassMap.RegisterClassMap<Name>(cm =>
                {
                    cm.AutoMap();
                    cm.MapCreator(n => new Name(n.Value));
                    cm.MapProperty(n => n.Value).SetElementName("Value");
                });
            }
            catch (ArgumentException)
            {
                // ClassMap already registered, ignore
            }
        }

        // Register BsonClassMap for SSN value object
        if (!BsonClassMap.IsClassMapRegistered(typeof(SSN)))
        {
            try
            {
                BsonClassMap.RegisterClassMap<SSN>(cm =>
                {
                    cm.AutoMap();
                    cm.MapCreator(s => new SSN(s.Value));
                    cm.MapProperty(s => s.Value).SetElementName("Value");
                });
            }
            catch (ArgumentException)
            {
                // ClassMap already registered, ignore
            }
        }

        // Register BsonClassMap for AccountCollection
        if (!BsonClassMap.IsClassMapRegistered(typeof(AccountCollection)))
        {
            try
            {
                BsonClassMap.RegisterClassMap<AccountCollection>(cm =>
                {
                    cm.AutoMap();
                    cm.MapField("_accountIds").SetElementName("AccountIds");
                    cm.SetIgnoreExtraElements(true);
                });
            }
            catch (ArgumentException)
            {
                // ClassMap already registered, ignore
            }
        }

        // Register BsonClassMap for Account - ignore Credits and Debits as they are stored in separate collections
        if (!BsonClassMap.IsClassMapRegistered(typeof(Account)))
        {
            try
            {
                BsonClassMap.RegisterClassMap<Account>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(a => a.Id);
                    cm.UnmapProperty(a => a.Credits);
                    cm.UnmapProperty(a => a.Debits);
                    cm.UnmapProperty(a => a.DomainEvents);
                    cm.SetIgnoreExtraElements(true);
                });
            }
            catch (ArgumentException)
            {
                // ClassMap already registered, ignore
            }
        }

        var pack = new ConventionPack
        {
            new IgnoreExtraElementsConvention(true),
            new IgnoreIfDefaultConvention(true)
        };

        ConventionRegistry.Register("Genocs Solution Conventions", pack, t => true);
    }

    public async Task<int> SaveChangesAsync()
    {
        int count = _commands.Count;
        CancellationToken token = new CancellationToken();

        // Do not support transactions if the cluster is a standalone server,
        // because transactions are only supported on replica sets and sharded clusters.
        // So call the commands without transaction if the cluster is a standalone server.
        if (MongoClient.Cluster.Description.Servers.Any(s => s.Type == ServerType.Standalone || s.Type == ServerType.Unknown))
        {
            var commandTasks = _commands.Select(c => c());

            await Task.WhenAll(commandTasks);
            _commands.Clear();
        }
        else
        {
            using (Session = await MongoClient.StartSessionAsync(options: null, cancellationToken: token))
            {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                // await Session.AbortTransactionAsync(token);

                await Session.CommitTransactionAsync();
                _commands.Clear();
                Session.Dispose();
                Session = null;
            }
        }

        return count;
    }

    private bool _disposed = false;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void AddCommand(Func<Task> func)
        => _commands.Add(func);

    public IMongoCollection<T> GetCollection<T>(string name)
        where T : IEntity<Guid>
        => _database.GetCollection<T>(name);

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            _disposed = true;
            if (disposing)
            {
                while (Session != null && Session.IsInTransaction)
                    Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
        }
    }
}