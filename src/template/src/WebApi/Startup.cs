using Asp.Versioning.ApiExplorer;
using Genocs.CleanArchitecture.Template.WebApi.ApiClient;
using Genocs.CleanArchitecture.Template.WebApi.Extensions;
using Genocs.CleanArchitecture.Template.WebApi.Extensions.FeatureFlags;
using Refit;

namespace Genocs.CleanArchitecture.Template.WebApi;

public sealed class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
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

        /*
         * ************************************************************
         * Persistence and Service Bus Sections
         * ************************************************************
        */

#if IN_MEMORY
        services.AddInMemoryPersistence();
#elif MONGO_DB
        services.AddMongoDBPersistence(Configuration);
#elif SQL_SERVER
        services.AddSQLServerPersistence(Configuration);
#endif

#if REBUS
        services.AddRebusServiceBus(Configuration);
#elif MASSTRANSIT
        services.AddMassTransitServiceBus(Configuration);
#elif N_SERVICE_BUS
        services.AddNServiceBusServiceBus(Configuration);
#endif

        /*
         * ************************************************************
         * Persistence and Service Bus Sections END
         * ************************************************************
        */

        services.AddPresentersV1();
        services.AddPresentersV2();

        // refit apis
        services.AddRefitClient<IOrderApi>()

        // .AddHttpMessageHandler<AuthorizationMessageHandler>()
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
}