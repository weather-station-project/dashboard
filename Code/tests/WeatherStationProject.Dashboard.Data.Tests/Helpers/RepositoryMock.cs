using Microsoft.EntityFrameworkCore;

namespace WeatherStationProject.Dashboard.Data.Tests
{
    public class RepositoryMock : Repository<MeasurementMock>
    {
        public RepositoryMock(DbContext weatherStationDatabaseContext) : base(weatherStationDatabaseContext)
        {
        }
    }
}