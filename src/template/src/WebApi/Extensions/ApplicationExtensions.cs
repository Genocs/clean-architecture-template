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
        services.AddScoped<Application.Boundaries.CloseAccount.IUseCase, CloseAccount>();
#endif
#if Deposit
        services.AddScoped<Application.Boundaries.Deposits.IUseCase, Deposit>();
#endif
#if GetAccountDetails
        services.AddScoped<Application.Boundaries.GetAccountDetails.IUseCase, GetAccountDetails>();
#endif
#if Refund
        services.AddScoped<Application.Boundaries.Refunds.IUseCase, Refund>();
#endif
#if GetCustomerDetails
        services.AddScoped<Application.Boundaries.GetCustomerDetails.IUseCase, GetCustomerDetails>();
#endif
#if Register
        services.AddScoped<Application.Boundaries.Registers.IUseCase, Register>();
#endif
#if Withdraw
        services.AddScoped<Application.Boundaries.Withdraws.IUseCase, Withdraw>();
#endif
#if Transfer
        services.AddScoped<Application.Boundaries.Transfers.IUseCase, Transfer>();
#endif
        return services;
    }
}
