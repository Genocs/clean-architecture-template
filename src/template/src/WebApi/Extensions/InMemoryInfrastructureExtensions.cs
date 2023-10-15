using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Repositories;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

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
