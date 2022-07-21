using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.RainfallService.Data;

namespace WeatherStationProject.Dashboard.RainfallService.Services
{
    public class RainfallService : IRainfallService
    {
        private readonly IRainfallRepository _repository;

        public RainfallService(IRainfallRepository repository)
        {
            _repository = repository;
        }

        public async Task<decimal> GetRainfallDuringTime(DateTime since, DateTime until)
        {
            return await _repository.GetRainfallDuringTime(since, until);
        }

        public async Task<List<Rainfall>> GetRainfallMeasurementsBetweenDates(DateTime since, DateTime until)
        {
            return await _repository.GetMeasurementsBetweenDates(since, until);
        }
    }
}