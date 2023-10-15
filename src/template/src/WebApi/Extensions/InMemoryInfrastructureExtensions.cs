namespace Genocs.MicroserviceLight.Template.WebApi.Extensions;

using Application.Repositories;
using Application.Services;
using Domain;
using Infrastructure.PersistenceLayer.InMemory;
using Infrastructure.PersistenceLayer.InMemory.Repositories;
using Microsoft.Extensions.DependencyInjection;

public static class InMemoryInfrastructureExtensions
{
    public static IServiceCollection AddInMemoryPersistence(this IServiceCollection services)
    {
        services.AddScoped<IEntityFactory, EntityFactory>();

        services.AddSingleton<GenocsContext, GenocsContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
