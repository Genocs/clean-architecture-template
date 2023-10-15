namespace Genocs.CleanArchitecture.Template.Infrastructure.WebApiClient.Resiliency
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Polly;
    using System;
    using System.Net.Http;

    public static class ResiliencyExtensions
    {
        public static IHttpClientBuilder AddResiliencyPolicies(this IHttpClientBuilder builder, IConfiguration configuration)
        {
            ResiliencyConfiguration config = new ResiliencyConfiguration();
            configuration.GetSection("ServiceRequestOptions").Bind(config);

            builder
                .AddPolicyHandler(
                    Policy.BulkheadAsync<HttpResponseMessage>(config.MaxBulkheadSize, config.MaxBulkheadQueueSize))
                .AddTransientHttpErrorPolicy(p =>
                    p.AdvancedCircuitBreakerAsync(
                        config.CircuitBreakerThreshold,
                        TimeSpan.FromSeconds(config.CircuitBreakerSamplingPeriodSeconds),
                        config.CircuitBreakerMinimumThroughput,
                        TimeSpan.FromSeconds(config.CircuitBreakerBreakDurationSeconds)))
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(config.MaxRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt) - 2)));

            return builder;
        }
    }
}
