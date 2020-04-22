namespace Genocs.MicroserviceLight.Template.WebApi.Extensions
{
    using Genocs.MicroserviceLight.Template.Application.Repositories;
    using Genocs.MicroserviceLight.Template.Application.Services;
    using Genocs.MicroserviceLight.Template.Domain;
    using Genocs.MicroserviceLight.Template.Infrastructure.InMemoryDataAccess;
    using Genocs.MicroserviceLight.Template.Infrastructure.InMemoryDataAccess.Repositories;
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