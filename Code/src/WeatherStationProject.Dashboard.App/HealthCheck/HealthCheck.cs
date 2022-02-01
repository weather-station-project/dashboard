using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WeatherStationProject.Dashboard.App.Handlers;

namespace WeatherStationProject.Dashboard.App.HealthCheck
{
    public class HealthCheck : IHealthCheck
    {
        private const string MeasurementsUrl = "/api/weather-measurements/last";
        private static readonly HttpMessageHandler HttpHandler = new SslIgnoreClientHandler();

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new())
        {
            try
            {
                using var client = new HttpClient(HttpHandler, false);
                var response =
                    await client.GetAsync(new Uri(Environment.GetEnvironmentVariable("ASPNETCORE_URLS") +
                                                  MeasurementsUrl), cancellationToken);
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