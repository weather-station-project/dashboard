using System;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;

namespace WeatherStationProject.Dashboard.GroundTemperatureService.ViewModel
{
    public class GroundTemperatureDTO
    {
        public DateTime DateTime { get; set; }

        public decimal Temperature { get; set; }

        public static GroundTemperatureDTO FromEntity(GroundTemperature entity)
        {
            return new GroundTemperatureDTO
            {
                DateTime = entity.DateTime,
                Temperature = entity.Temperature
            };
        }
    }
}