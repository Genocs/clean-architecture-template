namespace Genocs.MicroserviceLight.Template.WebApi.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class UserInterfaceV2Extensions
    {
        public static IServiceCollection AddPresentersV2(this IServiceCollection services)
        {
#if GetAccountDetails
            services.AddScoped<UseCases.V2.GetAccountDetails.GetAccountDetailsPresenterV2, UseCases.V2.GetAccountDetails.GetAccountDetailsPresenterV2>();

            services.AddTransient(ctx =>
                new UseCases.V2.GetAccountDetails.AccountsV2Controller(
                    new Application.UseCases.GetAccountDetails(
                        ctx.GetRequiredService<UseCases.V2.GetAccountDetails.GetAccountDetailsPresenterV2>(),
                        ctx.GetRequiredService<Application.Repositories.IAccountRepository>()
                    ),
                    ctx.GetRequiredService<UseCases.V2.GetAccountDetails.GetAccountDetailsPresenterV2>()
                )
            );
#endif
            return services;
        }
    }
}