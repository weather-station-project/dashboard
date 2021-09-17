using System.Threading.Tasks;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.WindMeasurementsService.Data
{
    public interface IWindMeasurementsRepository : IRepository<WindMeasurements>
    {
        Task<WindMeasurements> GetGustInTime(int minutes);
    }
}