using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.HealthCheck
{
    public class HealthCheck : IHealthCheck
    {
        private readonly GroundTemperatureDbContext _dbContext;
        
        public HealthCheck(GroundTemperatureDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new())
        {
            try
            {
                await _dbContext.GroundTemperatures.FirstOrDefaultAsync(cancellationToken);

                return await Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception e)
            {
                return await Task.FromResult(HealthCheckResult.Unhealthy(e.Message, e));
            }
        }
    }
}