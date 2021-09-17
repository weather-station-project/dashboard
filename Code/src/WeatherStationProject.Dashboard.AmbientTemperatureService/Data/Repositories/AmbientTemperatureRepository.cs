using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Data
{
    public class AmbientTemperatureRepository : Repository<AmbientTemperature>
    {
        public AmbientTemperatureRepository(AmbientTemperatureDbContext ambientTemperatureDbContext) : base(
            ambientTemperatureDbContext)
        {
        }
    }
}