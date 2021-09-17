using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeatherStationProject.Dashboard.Data
{
    public abstract class Repository<T> : IRepository<T> where T : Measurement
    {
        private readonly WeatherStationDatabaseContext _weatherStationDatabaseContext;

        protected Repository(WeatherStationDatabaseContext weatherStationDatabaseContext)
        {
            _weatherStationDatabaseContext = weatherStationDatabaseContext;
        }

        public async Task<T> GetLastMeasurement()
        {
            return await _weatherStationDatabaseContext.Set<T>().OrderByDescending(x => x.DateTime)
                .FirstOrDefaultAsync();
        }
    }
}