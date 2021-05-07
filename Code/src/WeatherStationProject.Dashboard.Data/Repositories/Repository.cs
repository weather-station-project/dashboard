using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherStationProject.Dashboard.Data
{
    public abstract class Repository<T> : IRepository<T> where T : Measurement
    {
        private readonly WeatherStationDatabaseContext _weatherStationDatabaseContext;

        protected Repository(WeatherStationDatabaseContext weatherStationDatabaseContext)
        {
            _weatherStationDatabaseContext = weatherStationDatabaseContext;
        }

        public async Task<List<T>> GetMeasurementsBetweenDatesAsync(DateTime since, DateTime until)
        {
            return await _weatherStationDatabaseContext.Set<T>().Where(x => x.DateTime >= since && x.DateTime <= until).ToListAsync();
        }
    }
}
