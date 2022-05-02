using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WeatherStationProject.Dashboard.AirParametersService.Data;

namespace WeatherStationProject.Dashboard.AirParametersService.HealthCheck
{
    public class HealthCheck : IHealthCheck
    {
        private readonly AirParametersDbContext _dbContext;
        
        public HealthCheck(AirParametersDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new())
        {
            try
            {
                await _dbContext.AirParameters.FirstOrDefaultAsync(cancellationToken);

                return await Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception e)
            {
                return await Task.FromResult(HealthCheckResult.Unhealthy(e.Message, e));
            }
        }
    }
}