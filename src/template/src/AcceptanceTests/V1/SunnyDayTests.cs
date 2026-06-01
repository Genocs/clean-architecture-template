using System.Text;
using Genocs.CleanArchitecture.Template.AcceptanceTests.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Genocs.CleanArchitecture.Template.AcceptanceTests.V1;

public sealed class SunnyDayTests(AcceptanceWebApplicationFactory factory) : IClassFixture<AcceptanceWebApplicationFactory>
{
    private readonly WebApplicationFactory<Program> _factory = factory;
#if Full
    [Fact]
    public async Task RegisterDepositWithdrawCloseAsync()
    {
        var customerId_accountId = await RegisterAsync(100);
        await GetCustomerAsync(customerId_accountId.Item1);
        await GetAccountAsync(customerId_accountId.Item2);
        await WithdrawAsync(customerId_accountId.Item2, 100);
        await GetCustomerAsync(customerId_accountId.Item1);
        await DepositAsync(customerId_accountId.Item2, 500);
        await DepositAsync(customerId_accountId.Item2, 400);
        await GetCustomerAsync(customerId_accountId.Item1);
        await WithdrawAsync(customerId_accountId.Item2, 400);
        await WithdrawAsync(customerId_accountId.Item2, 500);
        await CloseAsync(customerId_accountId.Item2);
    }

    private async Task GetCustomerAsync(string customerId)
    {
        var client = _factory.CreateClient();
        string result = await client.GetStringAsync($"/api/v1/Customers/{customerId}?api-version=1");
    }

    private async Task GetAccountAsync(string accountId)
    {
        var client = _factory.CreateClient();
        string result = await client.GetStringAsync($"/api/v1/Accounts/{accountId}?api-version=1");
    }

    private async Task<Tuple<string, string>> RegisterAsync(decimal initialAmount)
    {
        var client = _factory.CreateClient();
        var register = new
        {
            ssn = "8608179990",
            name = "Nocco Giovanni Emanuele",
            initialAmount
        };

        string registerData = JsonConvert.SerializeObject(register);
        StringContent content = new StringContent(registerData, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("api/v1/Customers?api-version=1", content);

        response.EnsureSuccessStatusCode();

        string responseString = await response.Content.ReadAsStringAsync();

        Assert.Contains("customerId", responseString);
        var customer = JsonConvert.DeserializeObject<JObject>(responseString);

        string customerId = string.Empty;
        string accountId = string.Empty;

        if (customer != null)
        {
            customerId = customer?["customerId"]?.Value<string>() ?? string.Empty;
            accountId = ((JContainer?)customer?["accounts"])?.First?["accountId"]?.Value<string>() ?? string.Empty;
        }

        return new Tuple<string, string>(customerId, accountId);
    }

    private async Task DepositAsync(string account, decimal amount)
    {
        var client = _factory.CreateClient();
        var json = new
        {
            accountId = account,
            amount,
        };

        string data = JsonConvert.SerializeObject(json);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

        var response = await client.PatchAsync("api/v1/Accounts/Deposit?api-version=1", content);
        string result = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();
    }

    private async Task WithdrawAsync(string account, decimal amount)
    {
        var client = _factory.CreateClient();
        var json = new
        {
            accountId = account,
            amount,
        };

        string data = JsonConvert.SerializeObject(json);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

        var response = await client.PatchAsync("api/v1/Accounts/Withdraw?api-version=1", content);
        string result = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();
    }

    private async Task CloseAsync(string account, CancellationToken cancellationToken = default)
    {
        var client = _factory.CreateClient();
        var response = await client.DeleteAsync($"api/v1/Accounts/{account}?api-version=1", cancellationToken);
        response.EnsureSuccessStatusCode();
    }
#endif
}