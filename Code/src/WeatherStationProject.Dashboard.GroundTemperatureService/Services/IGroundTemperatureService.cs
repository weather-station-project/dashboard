using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.Services
{
    public interface IGroundTemperatureService
    {
        Task<GroundTemperature> GetLastTemperature();

        Task<List<GroundTemperature>> GetAmbientTemperaturesBetweenDatesAsync(DateTime since, DateTime until);
    }
}
