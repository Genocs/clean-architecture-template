namespace Genocs.MicroserviceLight.Template.WebApi.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class UserInterfaceV1Extensions
    {
        public static IServiceCollection AddPresentersV1(this IServiceCollection services)
        {
            services.AddScoped<UseCases.V1.CloseAccount.CloseAccountPresenter, UseCases.V1.CloseAccount.CloseAccountPresenter>();
            services.AddScoped<Application.Boundaries.CloseAccount.IOutputPort>(x => x.GetRequiredService<UseCases.V1.CloseAccount.CloseAccountPresenter>());
            services.AddScoped<UseCases.V1.Deposit.DepositPresenter, UseCases.V1.Deposit.DepositPresenter>();
            services.AddScoped<Application.Boundaries.Deposit.IOutputPort>(x => x.GetRequiredService<UseCases.V1.Deposit.DepositPresenter>());
            services.AddScoped<UseCases.V1.GetAccountDetails.GetAccountDetailsPresenter, UseCases.V1.GetAccountDetails.GetAccountDetailsPresenter>();
            services.AddScoped<Application.Boundaries.GetAccountDetails.IOutputPort>(x => x.GetRequiredService<UseCases.V1.GetAccountDetails.GetAccountDetailsPresenter>());
            services.AddScoped<UseCases.V1.GetCustomerDetails.GetCustomerDetailsPresenter, UseCases.V1.GetCustomerDetails.GetCustomerDetailsPresenter>();
            services.AddScoped<Application.Boundaries.GetCustomerDetails.IOutputPort>(x => x.GetRequiredService<UseCases.V1.GetCustomerDetails.GetCustomerDetailsPresenter>());
            services.AddScoped<UseCases.V1.Register.RegisterPresenter, UseCases.V1.Register.RegisterPresenter>();
            services.AddScoped<Application.Boundaries.Register.IOutputPort>(x => x.GetRequiredService<UseCases.V1.Register.RegisterPresenter>());
            services.AddScoped<UseCases.V1.Withdraw.WithdrawPresenter, UseCases.V1.Withdraw.WithdrawPresenter>();
            services.AddScoped<Application.Boundaries.Withdraw.IOutputPort>(x => x.GetRequiredService<UseCases.V1.Withdraw.WithdrawPresenter>());
            services.AddScoped<UseCases.V1.Transfer.TransferPresenter, UseCases.V1.Transfer.TransferPresenter>();
            services.AddScoped<Application.Boundaries.Transfer.IOutputPort>(x => x.GetRequiredService<UseCases.V1.Transfer.TransferPresenter>());
            return services;
        }
    }
}