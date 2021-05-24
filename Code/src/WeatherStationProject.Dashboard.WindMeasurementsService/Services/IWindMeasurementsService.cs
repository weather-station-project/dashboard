using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Services
{
    public interface IWindMeasurementsService
    {
        Task<WindMeasurements> GetLastWindMeasurements();

        Task<List<WindMeasurements>> GetWindMeasurementsBetweenDatesAsync(DateTime since, DateTime until);

        Task<WindMeasurements> GetGustInTime(int minutes);
    }
}
