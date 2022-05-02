using System.Threading.Tasks;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Services
{
    public class WindMeasurementsService : IWindMeasurementsService
    {
        private readonly IWindMeasurementsRepository _repository;

        public WindMeasurementsService(IWindMeasurementsRepository repository)
        {
            _repository = repository;
        }

        public async Task<WindMeasurements> GetLastWindMeasurements()
        {
            return await _repository.GetLastMeasurement();
        }

        public async Task<WindMeasurements> GetGustInTime(int minutes)
        {
            return await _repository.GetGustInTime(minutes);
        }
    }
}