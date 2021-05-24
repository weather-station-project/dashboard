using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.AirParametersService.Data;

namespace WeatherStationProject.Dashboard.AirParametersService.Services
{
    public interface IAirParametersService
    {
        Task<AirParameters> GetLastAirParameters();

        Task<List<AirParameters>> GetAirParametersBetweenDatesAsync(DateTime since, DateTime until);
    }
}
