using Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.CloseAccount;
using Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Deposit;
using Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.GetAccountDetails;
using Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.GetCustomerDetails;
using Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Refund;
using Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Register;
using Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Transfer;
using Genocs.CleanArchitecture.Template.WebApi.UseCases.V1.Withdraw;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class UserInterfaceV1Extensions
{
    public static IServiceCollection AddPresentersV1(this IServiceCollection services)
    {

#if CloseAccount
        services.AddScoped<CloseAccountPresenter, CloseAccountPresenter>();
        services.AddScoped<Application.Boundaries.CloseAccount.IOutputPort>(x => x.GetRequiredService<CloseAccountPresenter>());
#endif
#if Deposit
        services.AddScoped<DepositPresenter, DepositPresenter>();
        services.AddScoped<Application.Boundaries.Deposits.IOutputPort>(x => x.GetRequiredService<DepositPresenter>());
#endif
#if GetAccountDetails
        services.AddScoped<GetAccountDetailsPresenter, GetAccountDetailsPresenter>();
        services.AddScoped<Application.Boundaries.GetAccountDetails.IOutputPort>(x => x.GetRequiredService<GetAccountDetailsPresenter>());
#endif
#if GetCustomerDetails
        services.AddScoped<GetCustomerDetailsPresenter, GetCustomerDetailsPresenter>();
        services.AddScoped<Application.Boundaries.GetCustomerDetails.IOutputPort>(x => x.GetRequiredService<GetCustomerDetailsPresenter>());
#endif
#if Register
        services.AddScoped<RegisterPresenter, RegisterPresenter>();
        services.AddScoped<Application.Boundaries.Registers.IOutputPort>(x => x.GetRequiredService<RegisterPresenter>());
#endif
#if Withdraw
        services.AddScoped<WithdrawPresenter, WithdrawPresenter>();
        services.AddScoped<Application.Boundaries.Withdraws.IOutputPort>(x => x.GetRequiredService<WithdrawPresenter>());
#endif
#if Refund
        services.AddScoped<RefundPresenter, RefundPresenter>();
        services.AddScoped<Application.Boundaries.Refunds.IOutputPort>(x => x.GetRequiredService<RefundPresenter>());
#endif
#if Transfer
        services.AddScoped<TransferPresenter, TransferPresenter>();
        services.AddScoped<Application.Boundaries.Transfers.IOutputPort>(x => x.GetRequiredService<TransferPresenter>());
#endif
        return services;
    }
}
