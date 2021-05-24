using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.Data;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.Services
{
    public class GroundTemperatureService : IGroundTemperatureService
    {
        private readonly IRepository<GroundTemperature> _repository;

        public GroundTemperatureService(IRepository<GroundTemperature> repository)
        {
            _repository = repository;
        }

        public async Task<GroundTemperature> GetLastTemperature()
        {
            return await _repository.GetLastMeasurement();
        }

        public async Task<List<GroundTemperature>> GetGroundTemperaturesBetweenDatesAsync(DateTime since, DateTime until)
        {
            return await _repository.GetMeasurementsBetweenDatesAsync(since: since, until: until);
        }
    }
}
