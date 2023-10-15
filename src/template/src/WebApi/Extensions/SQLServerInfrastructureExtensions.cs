using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.EntityFramework;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class SQLServerInfrastructureExtensions
{
    public static IServiceCollection AddSQLServerPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEntityFactory, EntityFactory>();

        services.AddDbContext<GenocsContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
