using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.Data
{
    public class GroundTemperatureRepository : Repository<GroundTemperature>
    {
        public GroundTemperatureRepository(GroundTemperaturesDbContext ambientTemperaturesDbContext) : base(ambientTemperaturesDbContext) { }
    }
}
