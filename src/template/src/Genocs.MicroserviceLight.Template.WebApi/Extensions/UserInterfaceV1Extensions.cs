namespace Genocs.MicroserviceLight.Template.WebApi.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class UserInterfaceV1Extensions
    {
        public static IServiceCollection AddPresentersV1(this IServiceCollection services)
        {

#if CloseAccount
            services.AddScoped<WebApi.UseCases.V1.CloseAccount.CloseAccountPresenter, WebApi.UseCases.V1.CloseAccount.CloseAccountPresenter>();
            services.AddScoped<Application.Boundaries.CloseAccount.IOutputPort>(x => x.GetRequiredService<WebApi.UseCases.V1.CloseAccount.CloseAccountPresenter>());
#endif
#if Deposit
            services.AddScoped<WebApi.UseCases.V1.Deposit.DepositPresenter, WebApi.UseCases.V1.Deposit.DepositPresenter>();
            services.AddScoped<Application.Boundaries.Deposit.IOutputPort>(x => x.GetRequiredService<WebApi.UseCases.V1.Deposit.DepositPresenter>());
#endif
#if GetAccountDetails
            services.AddScoped<WebApi.UseCases.V1.GetAccountDetails.GetAccountDetailsPresenter, WebApi.UseCases.V1.GetAccountDetails.GetAccountDetailsPresenter>();
            services.AddScoped<Application.Boundaries.GetAccountDetails.IOutputPort>(x => x.GetRequiredService<WebApi.UseCases.V1.GetAccountDetails.GetAccountDetailsPresenter>());
#endif
#if GetCustomerDetails
            services.AddScoped<WebApi.UseCases.V1.GetCustomerDetails.GetCustomerDetailsPresenter, WebApi.UseCases.V1.GetCustomerDetails.GetCustomerDetailsPresenter>();
            services.AddScoped<Application.Boundaries.GetCustomerDetails.IOutputPort>(x => x.GetRequiredService<WebApi.UseCases.V1.GetCustomerDetails.GetCustomerDetailsPresenter>());
#endif
#if Register
            services.AddScoped<WebApi.UseCases.V1.Register.RegisterPresenter, WebApi.UseCases.V1.Register.RegisterPresenter>();
            services.AddScoped<Application.Boundaries.Register.IOutputPort>(x => x.GetRequiredService<WebApi.UseCases.V1.Register.RegisterPresenter>());
#endif
#if Withdraw
            services.AddScoped<WebApi.UseCases.V1.Withdraw.WithdrawPresenter, WebApi.UseCases.V1.Withdraw.WithdrawPresenter>();
            services.AddScoped<Application.Boundaries.Withdraw.IOutputPort>(x => x.GetRequiredService<WebApi.UseCases.V1.Withdraw.WithdrawPresenter>());
#endif
#if Transfer
            services.AddScoped<WebApi.UseCases.V1.Transfer.TransferPresenter, WebApi.UseCases.V1.Transfer.TransferPresenter>();
            services.AddScoped<Application.Boundaries.Transfer.IOutputPort>(x => x.GetRequiredService<WebApi.UseCases.V1.Transfer.TransferPresenter>());
#endif
            return services;
        }
    }
}