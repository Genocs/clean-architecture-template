using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Genocs.MicroserviceLight.Template.BusHost
{
    using Configurations;

    internal static class ResiliencyExtensions
    {
        public static IHttpClientBuilder AddResiliencyPolicies(this IHttpClientBuilder builder, IConfiguration configuration)
        {
            var resiliencyConfiguration = configuration.GetSection("ServiceRequestOptions").Get<ResiliencyConfiguration>();

            builder
                .AddPolicyHandler(
                    Policy.BulkheadAsync<HttpResponseMessage>(resiliencyConfiguration.MaxBulkheadSize, resiliencyConfiguration.MaxBulkheadQueueSize))
                .AddTransientHttpErrorPolicy(p =>
                    p.AdvancedCircuitBreakerAsync(
                        resiliencyConfiguration.CircuitBreakerThreshold,
                        TimeSpan.FromSeconds(resiliencyConfiguration.CircuitBreakerSamplingPeriodSeconds),
                        resiliencyConfiguration.CircuitBreakerMinimumThroughput,
                        TimeSpan.FromSeconds(resiliencyConfiguration.CircuitBreakerBreakDurationSeconds)))
                .AddTransientHttpErrorPolicy(p =>
                    p.WaitAndRetryAsync(resiliencyConfiguration.MaxRetries, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt) - 2)));

            return builder;
        }
    }
}
