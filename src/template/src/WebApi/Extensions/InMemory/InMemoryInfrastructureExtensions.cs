using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Microsoft.EntityFrameworkCore.Storage;
using Genocs.CleanArchitecture.Template.Domain;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.InMemory.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions.InMemory;

public static class InMemoryInfrastructureExtensions
{
    private static readonly InMemoryDatabaseRoot InMemoryDatabaseRoot = new();

    public static IServiceCollection AddInMemoryPersistence(this IServiceCollection services)
    {
        services.AddScoped<IEntityFactory, EntityFactory>();

        services.AddDbContext<GenocsContext>(options =>
            options.UseInMemoryDatabase("Genocs.CleanArchitecture.Template", InMemoryDatabaseRoot));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }

    public static IServiceProvider UseInMemoryPersistence(this IServiceProvider serviceProvider)
    {
        return serviceProvider;
    }
}
