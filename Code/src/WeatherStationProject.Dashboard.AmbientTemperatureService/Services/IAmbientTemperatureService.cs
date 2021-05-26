using System.Threading.Tasks;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.Services
{
    public interface IAmbientTemperatureService
    {
        Task<AmbientTemperature> GetLastTemperature();
    }
}
