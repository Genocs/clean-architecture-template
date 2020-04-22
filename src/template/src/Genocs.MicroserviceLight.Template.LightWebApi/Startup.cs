namespace Genocs.MicroserviceLight.Template
{
    using Autofac;
    using Autofac.Extensions.DependencyInjection;
    using Consul;
    using Genocs.MicroserviceLight.Template.Domain;
    using Genocs.MicroserviceLight.Template.Messages.Commands;
    using Genocs.MicroserviceLight.Template.Messages.Events;
    using Genocs.MicroserviceLight.Template.Services;
    using Genocs.Microservices.Common;
    using Genocs.Microservices.Common.Consul;
    using Genocs.Microservices.Common.Dispatchers;
    using Genocs.Microservices.Common.Jaeger;
    using Genocs.Microservices.Common.Mongo;
    using Genocs.Microservices.Common.Mvc;
    using Genocs.Microservices.Common.RabbitMq;
    using Genocs.Microservices.Common.Redis;
    using Genocs.Microservices.Common.RestEase;
    using Genocs.Microservices.Common.Swagger;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Reflection;

    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc();
            services.AddSwaggerDocs();
            services.AddConsul();
            services.AddJaeger();
            services.AddOpenTracing();
            services.AddRedis();
            services.AddInitializers(typeof(IMongoDbInitializer));
            services.RegisterServiceForwarder<IMyExternalService>("myexternal-service");

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();
            builder.Populate(services);
            builder.AddDispatchers();
            builder.AddRabbitMq();
            builder.AddMongo();
            builder.AddMongoRepository<FooTemplate>("FooTemplates");

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime applicationLifetime, IConsulClient client,
            IStartupInitializer startupInitializer)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "local")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAllForwardedHeaders();
            app.UseSwaggerDocs();
            app.UseErrorHandler();
            app.UseServiceId();
            app.UseMvc();
            app.UseRabbitMq()
                .SubscribeCommand<CreateFooTemplate>(onError: (c, e) =>
                    new CreateFooTemplateRejected(c.Id, e.Message, e.Code))
                .SubscribeEvent<SignedUp>(@namespace: "identity");

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                client.Agent.ServiceDeregister(consulServiceId);
                Container.Dispose();
            });

            startupInitializer.InitializeAsync();
        }
    }
}
