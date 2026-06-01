using Genocs.CleanArchitecture.Template.Application.Interfaces;
using Genocs.CleanArchitecture.Template.Application.Services;
using Genocs.CleanArchitecture.Template.Application.UseCases;
using Genocs.CleanArchitecture.Template.Infrastructure.WebApiClient.ExternalServices;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {

        services.AddScoped<IApiClient, AuthApiClient>();

#if CloseAccount
        services.AddScoped<IUseCase<Application.Boundaries.CloseAccount.CloseAccountInput>, CloseAccount>();
#endif
#if Deposit
        services.AddScoped<IUseCase<Application.Boundaries.Deposits.DepositInput>, Deposit>();
#endif
#if GetAccountDetails
        services.AddScoped<IUseCase<Application.Boundaries.GetAccountDetails.GetAccountDetailsInput>, GetAccountDetails>();
#endif
#if Refund
        services.AddScoped<IUseCase<Application.Boundaries.Refunds.RefundInput>, Refund>();
#endif
#if GetCustomerDetails
        services.AddScoped<IUseCase<Application.Boundaries.GetCustomerDetails.GetCustomerDetailsInput>, GetCustomerDetails>();
#endif
#if Register
        services.AddScoped<IUseCase<Application.Boundaries.Registers.RegisterInput>, Register>();
#endif
#if Withdraw
        services.AddScoped<IUseCase<Application.Boundaries.Withdraws.WithdrawInput>, Withdraw>();
#endif
#if Transfer
        services.AddScoped<IUseCase<Application.Boundaries.Transfers.TransferInput>, Transfer>();
#endif
        return services;
    }
}
