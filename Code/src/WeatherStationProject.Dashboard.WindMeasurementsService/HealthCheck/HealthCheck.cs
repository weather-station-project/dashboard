using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.HealthCheck
{
    public class HealthCheck : IHealthCheck
    {
        private readonly WindMeasurementsDbContext _dbContext;
        
        public HealthCheck(WindMeasurementsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = new())
        {
            try
            {
                await _dbContext.WindMeasurements.FirstOrDefaultAsync(cancellationToken);

                return await Task.FromResult(HealthCheckResult.Healthy());
            }
            catch (Exception e)
            {
                return await Task.FromResult(HealthCheckResult.Unhealthy(e.Message, e));
            }
        }
    }
}