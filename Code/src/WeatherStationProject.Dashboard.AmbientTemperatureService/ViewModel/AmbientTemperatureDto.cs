using System;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;

namespace WeatherStationProject.Dashboard.AmbientTemperatureService.ViewModel
{
    public class AmbientTemperatureDTO
    {
        public DateTime DateTime { get; set; }

        public decimal Temperature { get; set; }

        public static AmbientTemperatureDTO FromEntity(AmbientTemperature entity)
        {
            return new AmbientTemperatureDTO
            {
                DateTime = entity.DateTime.ToLocalTime(),
                Temperature = entity.Temperature
            };
        }
    }
}