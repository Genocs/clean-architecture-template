using Genocs.CleanArchitecture.Template.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb;

public sealed class GenocsContext : IMongoContext
{
    private readonly IMongoDatabase _database;

    private readonly List<Func<Task>> _commands;

    public MongoClient MongoClient { get; set; }
    public IClientSessionHandle Session { get; set; }

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
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));

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
        where T : IEntity
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
