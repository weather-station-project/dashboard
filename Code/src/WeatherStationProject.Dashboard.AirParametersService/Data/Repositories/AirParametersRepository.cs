using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.AirParametersService.Data
{
    public class AirParametersRepository : Repository<AirParameters>
    {
        public AirParametersRepository(AirParametersDbContext ambientTemperaturesDbContext) : base(
            ambientTemperaturesDbContext)
        {
        }
    }
}