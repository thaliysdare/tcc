using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace tcc.web.HealthCheck
{
    public class SiteCheck : IHealthCheck
    {
        public readonly HttpClient _tccApi;
        public SiteCheck()
        {
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(HealthCheckResult.Healthy("Site OK"));
        }
    }

    public class APICheck : IHealthCheck
    {
        public readonly HttpClient _tccApi;
        public APICheck(IHttpClientFactory httpClientFactory)
        {
            _tccApi = httpClientFactory.CreateClient("tcc.api");
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = _tccApi.GetAsync("healthcheck/verificacao").Result;
            if (!response.IsSuccessStatusCode)
            {
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "API não está respondendo"));
            }

            return Task.FromResult(HealthCheckResult.Healthy("API OK"));
        }
    }

    public class BancoCheck : IHealthCheck
    {
        public readonly HttpClient _tccApi;
        public BancoCheck(IHttpClientFactory httpClientFactory)
        {
            _tccApi = httpClientFactory.CreateClient("tcc.api");
        }

        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = _tccApi.GetAsync("healthcheck/verificacao-banco").Result;
            if (!response.IsSuccessStatusCode)
            {
                return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "Banco não está respondendo"));
            }

            return Task.FromResult(HealthCheckResult.Healthy("Banco OK"));
        }
    }
}
