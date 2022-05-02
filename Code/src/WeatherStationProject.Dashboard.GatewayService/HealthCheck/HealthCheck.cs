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
            try
            {
                using var client = new HttpClient(_httpHandler, false);
                var response = await client.GetAsync(new Uri("https://localhost:1443/api/v1/weather-measurements/last"),
                    cancellationToken);
                
                // Unauthorized is good as the call is not authenticated and the endpoints were found properly.
                // The real e2e test is in the app, which call the gateway with authentication and it connects
                // to the rest of services.
                return response.StatusCode == HttpStatusCode.Unauthorized
                    ? await Task.FromResult(HealthCheckResult.Healthy())
                    : await Task.FromResult(HealthCheckResult.Unhealthy());
            }
            catch (Exception e)
            {
                return await Task.FromResult(HealthCheckResult.Unhealthy(e.Message, e));
            }
        }
    }
}