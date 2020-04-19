namespace Genocs.WebApi.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddScoped<Genocs.Application.Boundaries.CloseAccount.IUseCase, Genocs.Application.UseCases.CloseAccount>();
            services.AddScoped<Genocs.Application.Boundaries.Deposit.IUseCase, Genocs.Application.UseCases.Deposit>();
            services.AddScoped<Genocs.Application.Boundaries.GetAccountDetails.IUseCase, Genocs.Application.UseCases.GetAccountDetails>();
            services.AddScoped<Genocs.Application.Boundaries.GetCustomerDetails.IUseCase, Genocs.Application.UseCases.GetCustomerDetails>();
            services.AddScoped<Genocs.Application.Boundaries.Register.IUseCase, Genocs.Application.UseCases.Register>();
            services.AddScoped<Genocs.Application.Boundaries.Withdraw.IUseCase, Genocs.Application.UseCases.Withdraw>();
            services.AddScoped<Genocs.Application.Boundaries.Transfer.IUseCase, Genocs.Application.UseCases.Transfer>();
            return services;
        }
    }
}