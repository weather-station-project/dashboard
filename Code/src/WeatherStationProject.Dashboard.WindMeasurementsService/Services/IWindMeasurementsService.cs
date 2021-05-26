using System.Threading.Tasks;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Services
{
    public interface IWindMeasurementsService
    {
        Task<WindMeasurements> GetLastWindMeasurements();

        Task<WindMeasurements> GetGustInTime(int minutes);
    }
}
