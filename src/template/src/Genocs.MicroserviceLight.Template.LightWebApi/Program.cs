namespace Genocs.MicroserviceLight.Template
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Genocs.Microservices.Common.Logging;
    using Genocs.Microservices.Common.Metrics;
    using Genocs.Microservices.Common.Mvc;
    using Genocs.Microservices.Common.Vault;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseLogging()
                .UseVault()
                .UseLockbox()
                .UseAppMetrics();
    }
}