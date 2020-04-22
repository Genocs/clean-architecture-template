namespace Genocs.MicroserviceLight.Template.WebApi.Extensions
{
    using Genocs.MicroserviceLight.Template.Application.Repositories;
    using Genocs.MicroserviceLight.Template.Application.Services;
    using Genocs.MicroserviceLight.Template.Domain;
    using Genocs.MicroserviceLight.Template.Infrastructure.EntityFrameworkDataAccess;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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
}