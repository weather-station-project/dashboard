using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeatherStationProject.Dashboard.Data
{
    public abstract class Repository<T> : IRepository<T> where T : Measurement
    {
        private readonly DbContext _weatherStationDatabaseContext;

        protected Repository(DbContext weatherStationDatabaseContext)
        {
            _weatherStationDatabaseContext = weatherStationDatabaseContext;
        }

        public async Task<T> GetLastMeasurement()
        {
            return await _weatherStationDatabaseContext.Set<T>().OrderByDescending(x => x.DateTime)
                .FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetMeasurementsBetweenDates(DateTime since, DateTime until)
        {
            return await _weatherStationDatabaseContext.Set<T>()
                .Where(x => x.DateTime >= since && x.DateTime <= until)
                .OrderByDescending(x => x.DateTime)
                .ToListAsync();
        }
    }
}