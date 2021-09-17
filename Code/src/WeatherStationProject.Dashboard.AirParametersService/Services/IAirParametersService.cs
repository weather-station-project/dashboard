using System.Threading.Tasks;
using WeatherStationProject.Dashboard.AirParametersService.Data;

namespace WeatherStationProject.Dashboard.AirParametersService.Services
{
    public interface IAirParametersService
    {
        Task<AirParameters> GetLastAirParameters();
    }
}