using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherStationProject.Dashboard.Data
{
    public interface IRepository<T> where T : Measurement
    {
        Task<T> GetLastMeasurement();

        Task<List<T>> GetMeasurementsBetweenDatesAsync(DateTime since, DateTime until);
    }
}
