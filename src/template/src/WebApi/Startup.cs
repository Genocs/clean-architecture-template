using Genocs.CleanArchitecture.Template.WebApi.ApiClient;
using Genocs.CleanArchitecture.Template.WebApi.Extensions;
using Genocs.CleanArchitecture.Template.WebApi.Extensions.FeatureFlags;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Refit;

namespace Genocs.CleanArchitecture.Template.WebApi;

public sealed class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureDevelopmentServices(IServiceCollection services)
        => InternalConfiguration(services);

    public void ConfigureProductionServices(IServiceCollection services)
        => InternalConfiguration(services);

    public void ConfigureDockerServices(IServiceCollection services)
        => InternalConfiguration(services);


    private void InternalConfiguration(IServiceCollection services)
    {
        services.AddControllers().AddControllersAsServices();
        services.AddBusinessExceptionFilter();
        services.AddFeatureFlags(Configuration);
        services.AddVersioning();
        services.AddSwagger();
        services.AddUseCases();
#if DEBUG
        services.AddInMemoryPersistence();
#else
        // Select your Database
        // services.AddMongoDBPersistence(Configuration);
        // services.AddSQLServerPersistence(Configuration);
#endif

        services.AddPresentersV1();
        services.AddPresentersV2();

#if AzureServiceBus
        // services.AddAzureServiceBus(Configuration);
#endif

#if NServiceBus
        // services.AddParticularServiceBus(Configuration);
#endif

#if Rebus
        // services.AddRebusServiceBus(Configuration);
#endif

        // Select your Enterprise service bus library
        // services.AddAzureServiceBus(Configuration);
        // services.AddMassTransitServiceBus(Configuration);
        services.AddParticularServiceBus(Configuration);

        // services.AddRebusServiceBus(Configuration);

        // refit apis
        services.AddRefitClient<IOrderApi>()
        //.AddHttpMessageHandler<AuthorizationMessageHandler>()
          .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration["ExternalWebServices:Order"]));

        // HealthChecks(services, Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseVersionedSwagger(provider);
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCookiePolicy();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    public void HealthChecks(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks().AddMongoDb(
            mongodbConnectionString: "mongodb://localhost:27017",
            name: "MongoDB",
            failureStatus: HealthStatus.Unhealthy,
            tags: new string[] { "db", "mongoDB" });
    }
}