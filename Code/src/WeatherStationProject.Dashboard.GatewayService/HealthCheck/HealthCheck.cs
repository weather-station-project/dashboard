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
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new())
        {
            // The real e2e test is in the app, which call the gateway with authentication and it connects
            // to the rest of services.
            return await Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}