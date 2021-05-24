using System;
using System.Threading.Tasks;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.RainfallService.Data
{
    public interface IRainfallRepository : IRepository<Rainfall>
    {
        Task<int> GetRainfallDuringTime(DateTime since, DateTime until);
    }
}
