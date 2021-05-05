using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeatherStationProject.Dashboard.Data
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly WeatherStationDatabaseContext WeatherStationDatabaseContext;

        protected Repository(WeatherStationDatabaseContext weatherStationDatabaseContext)
        {
            WeatherStationDatabaseContext = weatherStationDatabaseContext;
        }

        public async Task<T> GetEntityByIdAsync(int id)
        {
            return await WeatherStationDatabaseContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
