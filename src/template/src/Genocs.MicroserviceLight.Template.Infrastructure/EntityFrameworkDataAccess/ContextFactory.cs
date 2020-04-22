namespace Genocs.MicroserviceLight.Template.Infrastructure.EntityFrameworkDataAccess
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;
    using System.IO;

    public sealed class ContextFactory : IDesignTimeDbContextFactory<GenocsContext>
    {
        public GenocsContext CreateDbContext(string[] args)
        {
            string connectionString = ReadDefaultConnectionStringFromAppSettings();

            var builder = new DbContextOptionsBuilder<GenocsContext>();
            builder.UseSqlServer(connectionString);
            return new GenocsContext(builder.Options);
        }

        private string ReadDefaultConnectionStringFromAppSettings()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Production.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            return connectionString;
        }
    }
}