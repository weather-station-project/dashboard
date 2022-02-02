using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.HealthCheck
{
    public class HealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new())
        {
            try
            {
                await using (var dbContext = new AmbientTemperatureDbContext())
                {
                    await dbContext.AmbientTemperatures.FirstOrDefaultAsync(cancellationToken);
                }

                return await Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception e)
            {
                return await Task.FromResult(HealthCheckResult.Unhealthy(e.Message, e));
            }
        }
    }
}