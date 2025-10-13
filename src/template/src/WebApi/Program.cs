using Genocs.CleanArchitecture.Template.WebApi.ApiClient;
using Genocs.CleanArchitecture.Template.WebApi.Extensions;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Refit;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((_, lc) => lc
    .WriteTo.Console());

// Get services and config
var services = builder.Services;

services.AddApplicationInsightsTelemetry();

services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, _) =>
{
    module.IncludeDiagnosticSourceActivities.Add("MassTransit");
});

services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

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

// Register the Swagger generator, defining 1 or more Swagger documents
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Genocs.CleanArchitecture.Template",
        Description = "The Genocs.CleanArchitecture.Template service. The API contains OpenAPI documentation. This useful when used with LangChain tools and agents.",
        TermsOfService = new Uri("https://www.genocs.com/sections/software.html"),
        Contact = new OpenApiContact
        {
            Name = "Giovanni Emanuele Nocco",
            Email = "giovanni.nocco@gmail.com",
            Url = new Uri("https://www.genocs.com"),
        },
        License = new OpenApiLicense
        {
            Name = "Use under MIT",
            Url = new Uri("https://opensource.org/license/mit/"),
        }
    });

    c.AddServer(new OpenApiServer() { Url = "https://localhost:5001", Description = "Local version for internal test" });
    c.AddServer(new OpenApiServer() { Url = "http://genocs.cleanarchitecture.template-service", Description = "Docker version to use within docker compose" });
    c.AddServer(new OpenApiServer() { Url = "https://genocs.cleanarchitecture.template.azurewebsites.net", Description = "Deploy on Azure" });

    c.CustomOperationIds(oid =>
    {
        if (!(oid.ActionDescriptor is ControllerActionDescriptor actionDescriptor))
        {
            return null; // default behavior
        }

        return oid.GroupName switch
        {
            "v1" => $"{actionDescriptor.ActionName}",
            _ => $"_{actionDescriptor.ActionName}", // default behavior
        };
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the Text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

    // Set the comments path for the Swagger JSON and UI.
    // var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    // var filePath = Path.Combine(System.AppContext.BaseDirectory, "api-documentation.xml");
    c.IncludeXmlComments("api-documentation.xml");

});

// Setup Database
#if InMemory
services.AddInMemoryPersistence();
#endif
#if SqlServer
//services.AddSQLServerPersistence(builder.Configuration);
#endif
#if MongoDb
services.AddMongoDBPersistence(builder.Configuration);
#endif

services.AddUseCases();

services.AddPresentersV1();
services.AddPresentersV2();

// Setup your Enterprise service bus library
#if AzureServiceBus
//services.AddAzureServiceBus(builder.Configuration);
#endif
#if MassTransit
services.AddMassTransitServiceBus(builder.Configuration);
#endif
#if NServiceBus
//services.AddParticularServiceBus(builder.Configuration);
#endif
#if Rebus
services.AddRebusServiceBus(builder.Configuration);
#endif

// refit apis
services.AddRefitClient<IOrderApi>()

  // .AddHttpMessageHandler<AuthorizationMessageHandler>()
  .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ExternalWebServices:Order"]));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCookiePolicy();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

Log.CloseAndFlush();
