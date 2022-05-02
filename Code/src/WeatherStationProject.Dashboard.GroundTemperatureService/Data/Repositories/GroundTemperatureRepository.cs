using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.Data
{
    public class GroundTemperatureRepository : Repository<GroundTemperature>
    {
        public GroundTemperatureRepository(GroundTemperatureDbContext groundTemperatureDbContext) : base(
            groundTemperatureDbContext)
        {
        }
    }
}