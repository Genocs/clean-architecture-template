using Genocs.CleanArchitecture.Template.Infrastructure.HealthChecks;
using Genocs.CleanArchitecture.Template.WebApi.ApiClient;
using Genocs.CleanArchitecture.Template.WebApi.Extensions;
using Genocs.CleanArchitecture.Template.WebApi.Extensions.FeatureFlags;
using Genocs.Core.Builders;
using Genocs.Logging;
using Genocs.WebApi.Swagger.Docs;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Refit;
using Serilog;

StaticLogger.EnsureInitialized();

var builder = WebApplication.CreateBuilder(args);

builder.Host
        .UseLogging();

// Use Genocs Core Builders to register services and build the container
builder
    .AddGenocs()
    //.AddOpenTelemetry()
    .AddSwaggerDocs()
    .Build();

// Get services and config
var services = builder.Services;

// services.AddApplicationInsightsTelemetry();

// services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, _) =>
// {
//     module.IncludeDiagnosticSourceActivities.Add("MassTransit");
// });

services.AddControllers().AddControllersAsServices();

services.AddBusinessExceptionFilter();
services.AddFeatureFlags(builder.Configuration);
services.AddVersioning();

services.AddCustomHealthChecks(builder.Configuration);

services.Configure<HealthCheckPublisherOptions>(options =>
{
    options.Delay = TimeSpan.FromSeconds(2);
    options.Predicate = check => check.Tags.Contains("ready");
});

// Setup Cors
services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.WithOrigins(
                            "https://localhost:5001",
                            "http://localhost:5000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
    });
});

// Setup Database
#if InMemory
services.AddInMemoryPersistence();
#elif MongoDb
services.AddMongoDBPersistence(builder.Configuration);
#elif EFcore
services.AddSQLServerPersistence(builder.Configuration);
#endif

// Setup your Enterprise service bus library
#if Rebus
services.AddRebusServiceBus(builder.Configuration);
#elif MassTransit
services.AddMassTransitServiceBus(builder.Configuration);
#elif NServiceBus
services.AddNServiceBusServiceBus(builder.Configuration);
#elif AzureServiceBus
services.AddAzureServiceBus(builder.Configuration);
#endif

services.AddUseCases();

services.AddPresentersV1();
services.AddPresentersV2();

// refit apis
services.AddRefitClient<IOrderApi>()

  // .AddHttpMessageHandler<AuthorizationMessageHandler>()
  .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ExternalWebServices:Order"]));

var app = builder.Build();

app.UseGenocs()
    .UseSwaggerDocs();

app.UseHttpsRedirection();

app.UseCookiePolicy();

//var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
//app.UseVersionedSwagger(provider);

app.MapControllers();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

await app.RunAsync();

await Log.CloseAndFlushAsync();

// Make the implicit Program class public so test projects can access it
public partial class Program;
