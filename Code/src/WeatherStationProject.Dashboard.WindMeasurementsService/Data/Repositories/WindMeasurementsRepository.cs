using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Data
{
    public class WindMeasurementsRepository : Repository<WindMeasurements>, IWindMeasurementsRepository
    {
        private readonly WindMeasurementsDbContext _windMeasurementsDbContext;

        public WindMeasurementsRepository(WindMeasurementsDbContext windMeasurementsDbContext) : base(windMeasurementsDbContext)
        {
            _windMeasurementsDbContext = windMeasurementsDbContext;
        }

        public async Task<WindMeasurements> GetGustInTime(int minutes)
        {
            var until = DateTime.Now;
            var since = until.AddMinutes(-minutes);

            return await _windMeasurementsDbContext.WindMeasurements
                .Where(x => x.DateTime >= since && x.DateTime <= until)
                .OrderByDescending(x => x.Speed).FirstOrDefaultAsync();
        }
    }
}
