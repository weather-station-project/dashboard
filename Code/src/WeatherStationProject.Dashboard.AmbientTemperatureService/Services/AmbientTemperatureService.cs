using System.Threading.Tasks;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Services
{
    public class AmbientTemperatureService : IAmbientTemperatureService
    {
        private readonly IRepository<AmbientTemperature> _repository;

        public AmbientTemperatureService(IRepository<AmbientTemperature> repository)
        {
            _repository = repository;
        }

        public async Task<AmbientTemperature> GetLastTemperature()
        {
            return await _repository.GetLastMeasurement();
        }
    }
}