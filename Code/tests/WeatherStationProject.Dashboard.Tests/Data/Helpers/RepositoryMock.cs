using Microsoft.EntityFrameworkCore;
using WeatherStationProject.Dashboard.Data;

namespace WeatherStationProject.Dashboard.Tests.Data;

public class RepositoryMock : Repository<MeasurementMock>
{
    public RepositoryMock(DbContext weatherStationDatabaseContext) : base(weatherStationDatabaseContext)
    {
    }
}