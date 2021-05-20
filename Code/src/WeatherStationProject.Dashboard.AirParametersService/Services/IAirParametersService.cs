using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.AirParametersService.Data;

namespace WeatherStationProject.Dashboard.AirParametersService.Services
{
    public interface IAirParametersService
    {
        Task<AirParameters> GetLastTemperature();

        Task<List<AirParameters>> GetAmbientTemperaturesBetweenDatesAsync(DateTime since, DateTime until);
    }
}
