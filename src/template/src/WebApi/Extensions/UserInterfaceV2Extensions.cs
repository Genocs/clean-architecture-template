using Genocs.CleanArchitecture.Template.Application.Repositories;
using Genocs.CleanArchitecture.Template.WebApi.UseCases.V2.GetAccountDetails;

namespace Genocs.CleanArchitecture.Template.WebApi.Extensions;

public static class UserInterfaceV2Extensions
{
    public static IServiceCollection AddPresentersV2(this IServiceCollection services)
    {
#if GetAccountDetails
        services.AddScoped<GetAccountDetailsPresenterV2, GetAccountDetailsPresenterV2>();

        services.AddTransient(ctx =>
            new AccountsV2Controller(
                new Application.UseCases.GetAccountDetails(
                    ctx.GetRequiredService<GetAccountDetailsPresenterV2>(),
                    ctx.GetRequiredService<IAccountRepository>()),
                ctx.GetRequiredService<GetAccountDetailsPresenterV2>()));
#endif
        return services;
    }
}
