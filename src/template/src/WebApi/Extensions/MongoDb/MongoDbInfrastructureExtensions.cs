using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb.Repositories;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions.MongoDb;

public static class MongoDbInfrastructureExtensions
{
    public static IServiceCollection AddMongoDbPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        GenocsContext.RegisterConventions();

        services.AddScoped<IEntityFactory, EntityFactory>();
        services.AddScoped<IMongoContext, GenocsContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }

    public static IServiceProvider UseMongoDbPersistence(this IServiceProvider serviceProvider)
    {
        return serviceProvider;
    }
}
