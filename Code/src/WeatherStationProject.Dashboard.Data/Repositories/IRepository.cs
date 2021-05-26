using System.Threading.Tasks;

namespace WeatherStationProject.Dashboard.Data
{
    public interface IRepository<T> where T : Measurement
    {
        Task<T> GetLastMeasurement();
    }
}
