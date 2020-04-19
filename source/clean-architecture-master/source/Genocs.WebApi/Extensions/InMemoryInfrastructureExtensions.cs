namespace Genocs.WebApi.Extensions
{
    using Genocs.Application.Repositories;
    using Genocs.Application.Services;
    using Genocs.Domain;
    using Genocs.Infrastructure.InMemoryDataAccess;
    using Genocs.Infrastructure.InMemoryDataAccess.Repositories;
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
}