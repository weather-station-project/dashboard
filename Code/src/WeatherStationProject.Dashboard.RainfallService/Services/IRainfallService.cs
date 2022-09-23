using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.RainfallService.Data;

namespace WeatherStationProject.Dashboard.RainfallService.Services
{
    public interface IRainfallService
    {
        Task<decimal> GetRainfallDuringTime(DateTime since, DateTime until);
        
        Task<List<Rainfall>> GetRainfallMeasurementsBetweenDates(DateTime since, DateTime until);
    }
}