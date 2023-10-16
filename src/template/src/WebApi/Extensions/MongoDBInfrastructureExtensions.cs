using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.MongoDb.Repositories;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class MongoDBInfrastructureExtensions
{
    public static IServiceCollection AddMongoDBPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Initialize the static conventions
        GenocsContext.RegisterConventions();

        services.AddScoped<IEntityFactory, EntityFactory>();
        services.AddScoped<IMongoContext, GenocsContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
