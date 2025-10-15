using System.Reflection;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Infrastructure.MassTransitSB;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class MassTransitInfrastructureExtensions
{
    public static IServiceCollection AddMassTransitServiceBus(this IServiceCollection services, IConfiguration config)
    {
        // Settings registration
        //        services.Configure<MassTransitSettings>(config.GetSection(MassTransitSettings.Position));

        services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

        var massTransitSettings = new MassTransitSettings();
        config.GetSection(MassTransitSettings.Position).Bind(massTransitSettings);

        services.AddSingleton(massTransitSettings);

        AddApplicationMassTransit(services, massTransitSettings);

        services.AddScoped<IServiceBusClient, MassTransitServiceBusClient>();

        return services;
    }

    private static void AddApplicationMassTransit(IServiceCollection services, MassTransitSettings settings)
    {
        // services.AddMediator();
        services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);

        services.AddMassTransit(x =>
        {
            x.ConfigureHealthCheckOptions(options =>
            {
                options.Name = "masstransit";
                options.MinimalFailureStatus = HealthStatus.Unhealthy;
                options.Tags.Add("health");
                options.Tags.Add("ready");
                options.Tags.Add("masstransit");
            });

            //x.AddRequestClient<SubmitIssuingCardRequest>();

            // Consumer
            x.AddConsumers(Assembly.GetExecutingAssembly());
            x.AddActivities(Assembly.GetExecutingAssembly());
            x.SetKebabCaseEndpointNameFormatter();

            // Transport RabbitMQ
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);

                //cfg.UseHealthCheck(context);
                cfg.Host(settings.HostName, settings.VirtualHost,
                    h =>
                    {
                        h.Username(settings.UserName);
                        h.Password(settings.Password);

                        //h.UseSsl(s =>
                        //{
                        //    s.Protocol = SslProtocols.Tls12;
                        //});
                    });
            });

            //// Persistence MongoDB
            //x.AddSagaStateMachine<CardIssuingSagaStateMachine, CardIssuingSagaState>().MongoDbRepository(c =>
            //{
            //    MongoDbOptions databaseSettings = new MongoDbOptions();
            //    configuration.GetSection(MongoDbOptions.Position).Bind(databaseSettings);
            //    c.Connection = databaseSettings.ConnectionString;
            //    c.DatabaseName = "Masstransit";

            //    // c.ClassMap(m => { });
            //});

            //x.AddSagaStateMachine<VoucherSagaStateMachine, VoucherSagaState>().MongoDbRepository(c =>
            //{
            //    MongoDbOptions databaseSettings = new MongoDbOptions();
            //    configuration.GetSection(MongoDbOptions.Position).Bind(databaseSettings);
            //    c.Connection = databaseSettings.ConnectionString;
            //    c.DatabaseName = "Masstransit";

            //    // c.ClassMap(m => { });
            //});
        });

        //services.AddScoped<SubmitCardRequestActivity>();
        //services.AddScoped<SubmitPackageVoucherActivity>();
        //services.AddScoped<SubmitIssuingVoucherActivity>();
    }
}
