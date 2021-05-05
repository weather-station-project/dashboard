using System.Threading.Tasks;

namespace WeatherStationProject.Dashboard.Data
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> GetEntityByIdAsync(int id);
    }
}
