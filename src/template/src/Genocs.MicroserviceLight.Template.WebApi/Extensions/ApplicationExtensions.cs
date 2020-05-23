namespace Genocs.MicroserviceLight.Template.WebApi.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
#if (CloseAccount)
            services.AddScoped<Application.Boundaries.CloseAccount.IUseCase, Application.UseCases.CloseAccount>();
#endif
#if (Deposit)
            services.AddScoped<Application.Boundaries.Deposit.IUseCase, Application.UseCases.Deposit>();
#endif
#if (GetAccountDetails)
            services.AddScoped<Application.Boundaries.GetAccountDetails.IUseCase, Application.UseCases.GetAccountDetails>();
#endif
#if (GetCustomerDetails)
            services.AddScoped<Application.Boundaries.GetCustomerDetails.IUseCase, Application.UseCases.GetCustomerDetails>();
#endif
#if (Register)
            services.AddScoped<Application.Boundaries.Register.IUseCase, Application.UseCases.Register>();
#endif
#if (Withdraw)
            services.AddScoped<Application.Boundaries.Withdraw.IUseCase, Application.UseCases.Withdraw>();
#endif
#if (Transfer)
            services.AddScoped<Application.Boundaries.Transfer.IUseCase, Application.UseCases.Transfer>();
#endif
            return services;
        }
    }
}