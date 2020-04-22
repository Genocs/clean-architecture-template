namespace Genocs.MicroserviceLight.Template.WebApi.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.CloseAccount.IUseCase, Genocs.MicroserviceLight.Template.Application.UseCases.CloseAccount>();
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.Deposit.IUseCase, Genocs.MicroserviceLight.Template.Application.UseCases.Deposit>();
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.GetAccountDetails.IUseCase, Genocs.MicroserviceLight.Template.Application.UseCases.GetAccountDetails>();
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.GetCustomerDetails.IUseCase, Genocs.MicroserviceLight.Template.Application.UseCases.GetCustomerDetails>();
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.Register.IUseCase, Genocs.MicroserviceLight.Template.Application.UseCases.Register>();
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.Withdraw.IUseCase, Genocs.MicroserviceLight.Template.Application.UseCases.Withdraw>();
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.Transfer.IUseCase, Genocs.MicroserviceLight.Template.Application.UseCases.Transfer>();
            return services;
        }
    }
}