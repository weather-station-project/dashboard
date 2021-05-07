using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Services
{
    public interface IAmbientTemperatureService
    {
        Task<List<AmbientTemperature>> GetAmbientTemperaturesBetweenDatesAsync(DateTime since, DateTime until);
    }
}
