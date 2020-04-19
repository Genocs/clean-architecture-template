namespace Genocs.WebApi.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class UserInterfaceV1Extensions
    {
        public static IServiceCollection AddPresentersV1(this IServiceCollection services)
        {
            services.AddScoped<Genocs.WebApi.UseCases.V1.CloseAccount.CloseAccountPresenter, Genocs.WebApi.UseCases.V1.CloseAccount.CloseAccountPresenter>();
            services.AddScoped<Genocs.Application.Boundaries.CloseAccount.IOutputPort>(x => x.GetRequiredService<Genocs.WebApi.UseCases.V1.CloseAccount.CloseAccountPresenter>());
            services.AddScoped<Genocs.WebApi.UseCases.V1.Deposit.DepositPresenter, Genocs.WebApi.UseCases.V1.Deposit.DepositPresenter>();
            services.AddScoped<Genocs.Application.Boundaries.Deposit.IOutputPort>(x => x.GetRequiredService<Genocs.WebApi.UseCases.V1.Deposit.DepositPresenter>());
            services.AddScoped<Genocs.WebApi.UseCases.V1.GetAccountDetails.GetAccountDetailsPresenter, Genocs.WebApi.UseCases.V1.GetAccountDetails.GetAccountDetailsPresenter>();
            services.AddScoped<Genocs.Application.Boundaries.GetAccountDetails.IOutputPort>(x => x.GetRequiredService<Genocs.WebApi.UseCases.V1.GetAccountDetails.GetAccountDetailsPresenter>());
            services.AddScoped<Genocs.WebApi.UseCases.V1.GetCustomerDetails.GetCustomerDetailsPresenter, Genocs.WebApi.UseCases.V1.GetCustomerDetails.GetCustomerDetailsPresenter>();
            services.AddScoped<Genocs.Application.Boundaries.GetCustomerDetails.IOutputPort>(x => x.GetRequiredService<Genocs.WebApi.UseCases.V1.GetCustomerDetails.GetCustomerDetailsPresenter>());
            services.AddScoped<Genocs.WebApi.UseCases.V1.Register.RegisterPresenter, Genocs.WebApi.UseCases.V1.Register.RegisterPresenter>();
            services.AddScoped<Genocs.Application.Boundaries.Register.IOutputPort>(x => x.GetRequiredService<Genocs.WebApi.UseCases.V1.Register.RegisterPresenter>());
            services.AddScoped<Genocs.WebApi.UseCases.V1.Withdraw.WithdrawPresenter, Genocs.WebApi.UseCases.V1.Withdraw.WithdrawPresenter>();
            services.AddScoped<Genocs.Application.Boundaries.Withdraw.IOutputPort>(x => x.GetRequiredService<Genocs.WebApi.UseCases.V1.Withdraw.WithdrawPresenter>());
            services.AddScoped<Genocs.WebApi.UseCases.V1.Transfer.TransferPresenter, Genocs.WebApi.UseCases.V1.Transfer.TransferPresenter>();
            services.AddScoped<Genocs.Application.Boundaries.Transfer.IOutputPort>(x => x.GetRequiredService<Genocs.WebApi.UseCases.V1.Transfer.TransferPresenter>());
            return services;
        }
    }
}