using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WeatherStationProject.Dashboard.GatewayService.HealthCheck
{
    public class HealthCheck : IHealthCheck
    {
        private readonly HttpMessageHandler _httpHandler;

        public HealthCheck(HttpMessageHandler handler)
        {
            _httpHandler = handler;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new())
        {
            return await Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}