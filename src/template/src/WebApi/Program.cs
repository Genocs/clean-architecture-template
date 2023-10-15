using Genocs.CleanArchitecture.Template.WebApi.ApiClient;
using Genocs.CleanArchitecture.Template.WebApi.Extensions;
using Microsoft.ApplicationInsights.DependencyCollector;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console());

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.ConfigureTelemetryModule<DependencyTrackingTelemetryModule>((module, o) =>
{
    module.IncludeDiagnosticSourceActivities.Add("MassTransit");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<HealthCheckPublisherOptions>(options =>
{
    options.Delay = TimeSpan.FromSeconds(2);
    options.Predicate = check => check.Tags.Contains("ready");
});

#if DEBUG
builder.Services.AddInMemoryPersistence();
//            builder.Services.AddMongoDBPersistence(Configuration);
#else
            builder.Services.AddSQLServerPersistence(Configuration);
#endif

// Select your Database

// builder.Services.AddInMemoryPersistence();
// builder.Services.AddMongoDBPersistence(Configuration);
// builder.Services.AddSQLServerPersistence(Configuration);
builder.Services.AddUseCases();

builder.Services.AddPresentersV1();
builder.Services.AddPresentersV2();


// Select your Enterprise service bus library

//builder.Services.AddAzureServiceBus(Configuration);
builder.Services.AddMassTransitServiceBus(builder.Configuration);
//builder.Services.AddParticularServiceBus(builder.Configuration);
// builder.Services.AddRebusServiceBus(builder.Configuration);

//refit apis
builder.Services.AddRefitClient<IOrderApi>()
  //.AddHttpMessageHandler<AuthorizationMessageHandler>()
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
