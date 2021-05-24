using System;
using System.Threading.Tasks;

namespace WeatherStationProject.Dashboard.RainfallService.Services
{
    public interface IRainfallService
    {
        Task<decimal> GetRainfallDuringTime(DateTime since, DateTime until);
    }
}
