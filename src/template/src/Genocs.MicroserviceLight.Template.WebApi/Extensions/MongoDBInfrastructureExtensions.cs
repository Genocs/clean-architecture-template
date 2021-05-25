namespace Genocs.MicroserviceLight.Template.WebApi.Extensions
{
    using Application.Repositories;
    using Application.Services;
    using Domain;
    using Infrastructure.PersistenceLayer.MongoDb;
    using Infrastructure.PersistenceLayer.MongoDb.Repositories;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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
}