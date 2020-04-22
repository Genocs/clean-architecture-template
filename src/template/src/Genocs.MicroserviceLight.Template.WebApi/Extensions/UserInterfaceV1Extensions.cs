namespace Genocs.MicroserviceLight.Template.WebApi.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class UserInterfaceV1Extensions
    {
        public static IServiceCollection AddPresentersV1(this IServiceCollection services)
        {
            services.AddScoped<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.CloseAccount.CloseAccountPresenter, Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.CloseAccount.CloseAccountPresenter>();
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.CloseAccount.IOutputPort>(x => x.GetRequiredService<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.CloseAccount.CloseAccountPresenter>());
            services.AddScoped<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Deposit.DepositPresenter, Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Deposit.DepositPresenter>();
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.Deposit.IOutputPort>(x => x.GetRequiredService<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Deposit.DepositPresenter>());
            services.AddScoped<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.GetAccountDetails.GetAccountDetailsPresenter, Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.GetAccountDetails.GetAccountDetailsPresenter>();
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.GetAccountDetails.IOutputPort>(x => x.GetRequiredService<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.GetAccountDetails.GetAccountDetailsPresenter>());
            services.AddScoped<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.GetCustomerDetails.GetCustomerDetailsPresenter, Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.GetCustomerDetails.GetCustomerDetailsPresenter>();
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.GetCustomerDetails.IOutputPort>(x => x.GetRequiredService<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.GetCustomerDetails.GetCustomerDetailsPresenter>());
            services.AddScoped<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Register.RegisterPresenter, Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Register.RegisterPresenter>();
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.Register.IOutputPort>(x => x.GetRequiredService<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Register.RegisterPresenter>());
            services.AddScoped<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Withdraw.WithdrawPresenter, Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Withdraw.WithdrawPresenter>();
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.Withdraw.IOutputPort>(x => x.GetRequiredService<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Withdraw.WithdrawPresenter>());
            services.AddScoped<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Transfer.TransferPresenter, Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Transfer.TransferPresenter>();
            services.AddScoped<Genocs.MicroserviceLight.Template.Application.Boundaries.Transfer.IOutputPort>(x => x.GetRequiredService<Genocs.MicroserviceLight.Template.WebApi.UseCases.V1.Transfer.TransferPresenter>());
            return services;
        }
    }
}