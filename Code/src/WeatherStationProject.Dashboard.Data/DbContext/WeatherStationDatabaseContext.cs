using Microsoft.EntityFrameworkCore;

namespace WeatherStationProject.Dashboard.Data
{
    public partial class WeatherStationDatabaseContext : DbContext
    {
        public WeatherStationDatabaseContext()
        {
        }

        public WeatherStationDatabaseContext(DbContextOptions<WeatherStationDatabaseContext> options) : base(options)
        {
        }
    }
}
