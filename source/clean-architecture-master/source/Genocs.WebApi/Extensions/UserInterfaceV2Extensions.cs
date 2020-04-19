namespace Genocs.WebApi.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class UserInterfaceV2Extensions
    {
        public static IServiceCollection AddPresentersV2(this IServiceCollection services)
        {
            services.AddScoped<Genocs.WebApi.UseCases.V2.GetAccountDetails.GetAccountDetailsPresenterV2, Genocs.WebApi.UseCases.V2.GetAccountDetails.GetAccountDetailsPresenterV2>();

            services.AddTransient(ctx =>
                new UseCases.V2.GetAccountDetails.AccountsV2Controller(
                    new Application.UseCases.GetAccountDetails(
                        ctx.GetRequiredService<Genocs.WebApi.UseCases.V2.GetAccountDetails.GetAccountDetailsPresenterV2>(),
                        ctx.GetRequiredService<Application.Repositories.IAccountRepository>()
                    ),
                    ctx.GetRequiredService<Genocs.WebApi.UseCases.V2.GetAccountDetails.GetAccountDetailsPresenterV2>()
                )
            );

            return services;
        }
    }
}