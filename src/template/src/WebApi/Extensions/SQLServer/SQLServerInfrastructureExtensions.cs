using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Domain;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.SQLServer;
using Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.SQLServer.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions.SQLServer;

public static class SQLServerInfrastructureExtensions
{
    private static readonly Lock MigrationLock = new();

    public static IServiceCollection AddSQLServerPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEntityFactory, EntityFactory>();

        services.AddDbContext<GenocsContext>(options =>
            options
                .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning))
                .UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b =>
                    {
                        b.MigrationsAssembly("Genocs.CleanArchitecture.Template.Infrastructure");
                        b.EnableRetryOnFailure();
                    }));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }

    public static IServiceProvider UseSqlServerPersistence(this IServiceProvider serviceProvider)
    {
        const int maxAttempts = 5;

        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                using var scope = serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetService<GenocsContext>();

                if (context?.Database.IsSqlServer() != true)
                {
                    return serviceProvider;
                }

                lock (MigrationLock)
                {
                    // CreateExecutionStrategy retries transient failures while creating/migrating a fresh database.
                    var strategy = context.Database.CreateExecutionStrategy();
                    strategy.Execute(() => context.Database.Migrate());
                }

                if (context.Database.CanConnect())
                {
                    return serviceProvider;
                }
            }
            catch (SqlException) when (attempt < maxAttempts)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(250 * attempt));
            }
        }

        // Last attempt should surface an actionable exception instead of silently starting with a broken DB.
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetService<GenocsContext>();
            if (context?.Database.IsSqlServer() == true)
            {
                lock (MigrationLock)
                {
                    var strategy = context.Database.CreateExecutionStrategy();
                    strategy.Execute(() => context.Database.Migrate());
                }
            }
        }

        return serviceProvider;
    }
}
