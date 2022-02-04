using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WeatherStationProject.Dashboard.Core.Configuration;

namespace WeatherStationProject.Dashboard.AuthenticationService.HealthCheck
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
                var response = await client.GetAsync(new Uri("https://localhost:1443/api/v1/authentication/" +
                                                             AppConfiguration.AuthenticationSecret),
                    cancellationToken);
                response.EnsureSuccessStatusCode();

                return await Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception e)
            {
                return await Task.FromResult(HealthCheckResult.Unhealthy(e.Message, e));
            }
        }
    }
}