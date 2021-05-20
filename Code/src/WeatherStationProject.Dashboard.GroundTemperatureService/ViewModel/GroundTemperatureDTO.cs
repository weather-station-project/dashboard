using System;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.ViewModel
{
    public class GroundTemperatureDTO
    {
        public DateTime DateTime { get; set; }

        public int Temperature { get; set; }

        public static GroundTemperatureDTO FromEntity(GroundTemperature entity)
        {
            return new()
            {
                DateTime = entity.DateTime,
                Temperature = entity.Temperature
            };
        }
    }
}
