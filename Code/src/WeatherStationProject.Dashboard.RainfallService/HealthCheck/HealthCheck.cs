using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WeatherStationProject.Dashboard.RainfallService.Data;

namespace WeatherStationProject.Dashboard.RainfallService.HealthCheck
{
    public class HealthCheck : IHealthCheck
    {
        private readonly RainfallDbContext _dbContext;
        
        public HealthCheck(RainfallDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new())
        {
            try
            {
                await _dbContext.Rainfall.FirstOrDefaultAsync(cancellationToken);

                return await Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception e)
            {
                return await Task.FromResult(HealthCheckResult.Unhealthy(e.Message, e));
            }
        }
    }
}