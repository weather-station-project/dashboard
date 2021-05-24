using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.AirParametersService.Data;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.AirParametersService.Services
{
    public class AirParametersService : IAirParametersService
    {
        private readonly IRepository<AirParameters> _repository;

        public AirParametersService(IRepository<AirParameters> repository)
        {
            _repository = repository;
        }

        public async Task<AirParameters> GetLastAirParameters()
        {
            return await _repository.GetLastMeasurement();
        }

        public async Task<List<AirParameters>> GetAirParametersBetweenDatesAsync(DateTime since, DateTime until)
        {
            return await _repository.GetMeasurementsBetweenDatesAsync(since: since, until: until);
        }
    }
}
