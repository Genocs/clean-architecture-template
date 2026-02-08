using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Genocs.CleanArchitecture.Template.Infrastructure.PersistenceLayer.EntityFramework;

public sealed class ContextFactory : IDesignTimeDbContextFactory<GenocsContext>
{
    public GenocsContext CreateDbContext(string[] args)
    {
        string? connectionString = ReadDefaultConnectionStringFromAppSettings();

        var builder = new DbContextOptionsBuilder<GenocsContext>();
        builder.UseSqlServer(connectionString);
        return new GenocsContext(builder.Options);
    }

    private static string? ReadDefaultConnectionStringFromAppSettings()
    {
        var configuration = new ConfigurationBuilder()
            .Build();

        return configuration.GetConnectionString("DefaultConnection");
    }
}