using System.Threading.Tasks;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.Services
{
    public interface IGroundTemperatureService
    {
        Task<GroundTemperature> GetLastTemperature();
    }
}